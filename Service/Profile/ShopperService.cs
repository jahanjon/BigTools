using AutoMapper;
using Common.Enums;
using Domain;
using Domain.Dto.Common;
using Domain.Dto.File;
using Domain.Dto.Product;
using Domain.Dto.Shopper;
using Domain.Entity.File;
using Domain.Entity.Identity;
using Domain.Entity.Place;
using Domain.Entity.Product;
using Domain.Entity.Profile;
using Domain.Enums;
using Domain.Repository.Base;
using Domain.Repository.Category;
using Domain.Repository.Identity;
using Domain.Repository.Profile;
using Domain.Service;
using Domain.Service.Profile;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Service.Profile;

public class ShopperService(
    IShopperRepository repository,
    IUserRepository userRepository,
    ICategoryLevel1Repository categoryRepository,
    IRepository<City> cityRepository,
    UserManager<User> userManager,
    IRepository<Brand> brandRepository,
    IMapper mapper,
    IStringLocalizer<ShopperService> localizer,
    IFileService fileService,
    IRepository<ShopperBrand> shopperBrandRepository,
    IRepository<FileRelation> fileRelationRepository)
    : IShopperSevice
{
    public async Task<ServiceResult> CreateAsync(ShopperCreateDto dto, int userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(cancellationToken, userId);
        var city = await cityRepository.GetByIdAsync(cancellationToken, dto.CityId);
        var categories = await categoryRepository.GetByIdsAsync(dto.CategoryIds, cancellationToken);

        if (!string.IsNullOrEmpty(dto.NationCode))
        {
            var isNationalIdUnique = await repository.IsNationalIdUniqueAsync(dto.NationCode, cancellationToken);
            if (!isNationalIdUnique)
                return new ServiceResult(false, ApiResultStatusCode.BadRequest, localizer["NationalIdIsNotUnique"]);
        }

        var newShopper = mapper.Map<Shopper>(dto);

        newShopper.Mobile = user.Mobile;
        newShopper.User = user;
        newShopper.City = city;
        newShopper.Categories = categories;
        newShopper.HomeAddress = "";
        newShopper.HomePostalCode = "";
        newShopper.LicenseCode = "";

        await repository.AddAsync(newShopper, cancellationToken);
        await userManager.AddToRoleAsync(user, "Shopper");

        user.IsNewUser = false;
        await userRepository.UpdateAsync(user, cancellationToken);

        var files = new List<FileRelation>();

        if (dto.DocOrRentImage != null)
        {
            var image = new FileRelation()
            {
                EntityId = newShopper.Id,
                EntityTypeName = nameof(Shopper),
                FileId = dto.DocOrRentImage.Value,
                Enabled = true,
                CreatedAt = DateTime.UtcNow
            };
            files.Add(image);
        }

        await fileRelationRepository.AddRangeAsync(files, cancellationToken);

        var shopperBrands = new List<ShopperBrand>();
        var brands = await brandRepository.Table.Where(b => dto.BrandIds.Contains(b.Id)).ToListAsync();
        foreach (var brand in brands)
        {
            shopperBrands.Add(new ShopperBrand()
            {
                ShopperId = newShopper.Id,
                Shopper = newShopper,
                BrandId = brand.Id,
                Brand = brand,
            });
        }
        await shopperBrandRepository.AddRangeAsync(shopperBrands, cancellationToken);

        return new ServiceResult(localizer["ShopperCreated"]);
    }


    public async Task<ServiceResult<PagedListDto<ShopperListDto>>> GetListAsync(RequestedPageDto<ShopperListFilterDto> dto, CancellationToken cancellationToken)
    {
        var result = await repository.GetListAsync(dto, cancellationToken);

        return new ServiceResult<PagedListDto<ShopperListDto>>(result);
    }

    public async Task<ServiceResult> ToggleActivateAsync(int id, CancellationToken cancellationToken)
    {
        var userId = await repository.GetUserIdByShopperId(id, cancellationToken);

        var result = await userRepository.ToggleActivateAsync(userId, cancellationToken);

        var message = result ? localizer["ShopperActivate"] : localizer["ShopperDeActivate"];

        return new ServiceResult(true, ApiResultStatusCode.Success, message);
    }

    public async Task<ServiceResult<ShopperFullDto, string>> GetAsync(int id, int userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(cancellationToken, userId);
        var isUserAdmin = await userManager.IsInRoleAsync(user, "Admin");
        if (!isUserAdmin)
        {
            var userIdByShopperId = await repository.GetUserIdByShopperId(id, cancellationToken);
            if (userIdByShopperId != userId)
            {
                return new ServiceResult<ShopperFullDto, string>(errors: localizer["ShopperNotFound"]);
            }
        }

        var shopper = await repository.GetWithDetailsAsync(id, cancellationToken);

        var shopperFullDto = mapper.Map<ShopperFullDto>(shopper);

        if (shopper.HomeCityId > 0)
        {
            var homeCity = await cityRepository.TableNoTracking
            .Where(c => c.Id == shopper.HomeCityId).Include(c => c.Province).SingleOrDefaultAsync();

            shopperFullDto.HomeCity = new IdTitleDto() { Id = homeCity.Id, Title = homeCity.Name };
            shopperFullDto.HomeProvince = new IdTitleDto() { Id = homeCity.ProvinceId, Title = homeCity.Province.Name };
        }

        var brands = await shopperBrandRepository.TableNoTracking
            .Where(shb => shb.ShopperId == id)
            .Select(shb => new BrandSummaryDto()
            {
                Id = shb.BrandId,
                Name = shb.Brand.Name,
                LogoFileGuid = shb.Brand.LogoFileGuid
            })
            .ToListAsync();

        foreach (var brand in brands)
        {
            if (brand.LogoFileGuid != null)
                brand.LogoFileLink = (await fileService.GetLinkAsync(
                    brand.LogoFileGuid, FileType.BrandLogo, cancellationToken)).Data;
        }

        shopperFullDto.Brands = brands;

        if (shopper.LicenseImage != null)
        {
            shopperFullDto.LicenseImage = new FileDto() { FileId = shopper.LicenseImage, FileType = FileType.ShopperLicense };
            shopperFullDto.LicenseImage.Link = (await fileService.GetLinkAsync(shopper.LicenseImage.Value, FileType.ShopperLicense, cancellationToken)).Data;
        }


        var docOrRentFiles = await fileRelationRepository.TableNoTracking
            .Where(f => f.EntityTypeName == nameof(Shopper) && f.EntityId == id && f.File.Type == FileType.ShopperDocOrRent)
            .Select(f => new FileDto()
            {
                FileId = f.FileId,
                FileType = f.File.Type
            }).ToListAsync();

        foreach (var file in docOrRentFiles)
        {
            file.Link = (await fileService.GetLinkAsync(file.FileId.Value, file.FileType.Value, cancellationToken)).Data;
        }

        shopperFullDto.DocOrRentImages = docOrRentFiles;

        var bannerFiles = await fileRelationRepository.TableNoTracking
            .Where(e => e.EntityTypeName == nameof(Shopper) && e.EntityId == id
            && e.File.Type != FileType.ShopperLicense && e.File.Type != FileType.ShopperDocOrRent)
            .Select(e => new FileDto
            {
                FileId = e.FileId,
                FileType = e.File.Type
            }).ToListAsync();

        foreach (var file in bannerFiles)
        {
            file.Link = (await fileService.GetLinkAsync(file.FileId.Value, file.FileType.Value, cancellationToken)).Data;
        }

        shopperFullDto.BannerImages = bannerFiles;

        return new ServiceResult<ShopperFullDto, string>(shopperFullDto);
    }

    public async Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(string keyword, int userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(cancellationToken, userId);
        var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
        var result = await repository.GetAllAsync(keyword, userId, isAdmin, cancellationToken);
        return new ServiceResult<Dictionary<int, string>>(result);
    }

    private async Task<ServiceResult> ValidateAsync(ShopperCreateDto dto, int userId, CancellationToken cancellationToken)
    {
        
        var isNationalIdUnique = await repository.IsNationalIdUniqueAsync(dto.NationCode, cancellationToken);
        return !isNationalIdUnique
            ? new ServiceResult(false, ApiResultStatusCode.BadRequest, localizer["NationalIdIsNotUnique"])
            : new ServiceResult();
    }

    public async Task<ServiceResult> UpdateAsync(ShopperUpdateDto dto, int userId, CancellationToken cancellationToken)
    {
        var shopper = await repository.Table
            .Where(sh => sh.Id == dto.Id)
            .Include(sh => sh.City)
            .Include(sh => sh.Categories)
            .SingleOrDefaultAsync();

        if (shopper == null)
            return new ServiceResult(false, ApiResultStatusCode.NotFound, localizer["ShopperNotFound"]);

        bool isAdmin = false;
        var loginUser = await userRepository.GetByIdAsync(cancellationToken, userId);
        if (loginUser != null)
            isAdmin = await userManager.IsInRoleAsync(loginUser, "Admin");

        if (shopper.UserId != userId && !isAdmin)
            return new ServiceResult(false, ApiResultStatusCode.UnAuthorized, localizer["UnAuthorized"]);

        if (!string.IsNullOrEmpty(dto.NationCode))
        {
            var isNationalIdUnique = await repository.IsNationalIdUniqueAsync(dto.NationCode, cancellationToken);
            if (!isNationalIdUnique)
            {
                new ServiceResult(false, ApiResultStatusCode.BadRequest, localizer["NationalIDIsNotUnique."]);
            }
            shopper.NationCode = dto.NationCode;
        }

        if (dto.CityId > 0)
        {
            var city = await cityRepository.GetByIdAsync(cancellationToken, dto.CityId);
            if( city == null)
                return new ServiceResult(false, ApiResultStatusCode.NotFound, localizer["CityNotFound"]);
            shopper.City = city;
            shopper.CityId = city.Id;
        }

        if (dto.HomeCityId > 0)
        {
            var homeCity = await cityRepository.GetByIdAsync(cancellationToken, dto.HomeCityId);
            if (homeCity == null)
                return new ServiceResult(false, ApiResultStatusCode.NotFound, localizer["CityNotFound"]);
            shopper.HomeCityId = homeCity.Id;
        }

        shopper.PersonName = dto.PersonName ?? shopper.PersonName;
        shopper.Phone = dto.Phone ?? shopper.Phone;
        shopper.BirthDate = dto.BirthDate ?? shopper.BirthDate;
        shopper.HomePostalCode = dto.HomePostalCode ?? shopper.HomePostalCode;
        shopper.HomeAddress = dto.HomeAddress ?? shopper.HomeAddress;
        shopper.Name = dto.Name ?? shopper.Name;
        shopper.PostalCode = dto.PostalCode ?? shopper.PostalCode;
        shopper.Address = dto.Address ?? shopper.Address;
        shopper.IsRent = dto.IsRent ?? shopper.IsRent;
        shopper.Area = dto.Area ?? shopper.Area;
        shopper.HasLicense = dto.HasLicense ?? shopper.HasLicense;
        shopper.LicenseCode = dto.LicenseCode ?? shopper.LicenseCode;
        shopper.IsRetail = dto.IsRetail ?? shopper.IsRetail;
        shopper.LicenseImage = dto.LicenseImage ?? shopper.LicenseImage;


        if (dto.CategoryIdsEdited)
        {
            var categories = await categoryRepository.Table.Where(x => dto.CategoryIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            shopper.Categories = categories;
        }

        await repository.UpdateAsync(shopper, cancellationToken);

        if (dto.DocOrRentImagesEdited)
        {
            var addFiles = new List<FileRelation>();
            var removeFiles = new List<FileRelation>();

            var docOrRents = await fileRelationRepository.Table
                .Where(f => f.EntityTypeName == nameof(Shopper) && f.EntityId == dto.Id
                && f.File.Type == FileType.ShopperDocOrRent).ToListAsync();

            var docOrRentIds = docOrRents.Select(e => e.FileId).ToList();

            foreach (var item in dto.DocOrRentImages)
            {
                if (!docOrRentIds.Contains(item))
                {
                    addFiles.Add(new FileRelation()
                    {
                        EntityTypeName = nameof(Shopper),
                        EntityId = dto.Id,
                        FileId = item,
                        Enabled = true,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }
            foreach (var item in docOrRents)
            {
                if (!dto.DocOrRentImages.Contains(item.FileId))
                {
                    removeFiles.Add(item);
                }
            }
            await fileRelationRepository.AddRangeAsync(addFiles, cancellationToken);
            await fileRelationRepository.DeleteRangeAsync(removeFiles, cancellationToken);
        }

        if (dto.BrandIdsEdited)
        {
            var addListBrands = new List<ShopperBrand>();
            var removeListBrands = new List<ShopperBrand>();

            var brands = await shopperBrandRepository.Entities
                .Where(e => e.ShopperId == shopper.Id).ToListAsync();

            var brandIds = brands.Select(e => e.BrandId).ToList();

            foreach (var item in dto.BrandIds)
            {
                if (!brandIds.Contains(item))
                {
                    var brand = await brandRepository.GetByIdAsync(cancellationToken, item);
                    if (brand != null)
                    {
                        addListBrands.Add(new ShopperBrand
                        {
                            BrandId = brand.Id,
                            Brand = brand,
                            Shopper = shopper,
                            ShopperId = shopper.Id,
                        });
                    }
                }
            }
            foreach (var item in brands)
            {
                if (!dto.BrandIds.Contains(item.BrandId))
                {
                    removeListBrands.Add(item);
                }
            }

            await shopperBrandRepository.AddRangeAsync(addListBrands, cancellationToken);
            await shopperBrandRepository.DeleteRangeAsync(removeListBrands, cancellationToken);
        }

        return new ServiceResult(true, ApiResultStatusCode.Success, localizer["ShopperUpdated"]);
    }

    public async Task<ServiceResult> AddFileAsync(ShopperFileDto dto, int userId, CancellationToken cancellationToken)
    {
        var shopper = await repository.TableNoTracking.Where(e => e.Id == dto.ShopperId).SingleOrDefaultAsync();

        if (shopper == null)
        {
            return new ServiceResult(false, ApiResultStatusCode.NotFound, localizer["ShopperNotFound"]);
        }

        bool isAdmin = false;
        var loginUser = await userRepository.GetByIdAsync(cancellationToken, userId);
        if (loginUser != null)
            isAdmin = await userManager.IsInRoleAsync(loginUser, "Admin");

        if (shopper.UserId != userId && !isAdmin)
        {
            return new ServiceResult(false, ApiResultStatusCode.UnAuthorized, localizer["UnAuthorized"]);
        }

        var files = new List<FileRelation>();
        foreach (var item in dto.FileIds)
        {
            files.Add(new FileRelation
            {
                EntityId = shopper.Id,
                EntityTypeName = nameof(Shopper),
                FileId = item,
                Enabled = true,
                CreatedAt = DateTime.UtcNow
            });
        }

        await fileRelationRepository.AddRangeAsync(files, cancellationToken);

        return new ServiceResult(true, ApiResultStatusCode.Success, "");
    }


    public async Task<ServiceResult> RemoveFileAsync(ShopperFileDto dto, int userId, CancellationToken cancellationToken)
    {
        var shopper = await repository.TableNoTracking.Where(e => e.Id == dto.ShopperId).SingleOrDefaultAsync();

        if (shopper == null)
        {
            return new ServiceResult(false, ApiResultStatusCode.NotFound, localizer["ShopperNotFound"]);
        }

        bool isAdmin = false;
        var loginUser = await userRepository.GetByIdAsync(cancellationToken, userId);
        if (loginUser != null)
            isAdmin = await userManager.IsInRoleAsync(loginUser, "Admin");

        if (shopper.UserId != userId && !isAdmin)
        {
            return new ServiceResult(false, ApiResultStatusCode.UnAuthorized, localizer["UnAuthorized"]);
        }

        var files = await fileRelationRepository.Table
            .Where(e => dto.FileIds.Contains(e.FileId) && e.EntityTypeName == nameof(Shopper) && e.EntityId == shopper.Id)
            .ToListAsync();

        await fileRelationRepository.DeleteRangeAsync(files, cancellationToken);
        return new ServiceResult(true, ApiResultStatusCode.Success, "");
    }


    public async Task<ServiceResult> UpdateMainFileAsync(ShopperFileUpdateDto dto, int userId, CancellationToken cancellationToken)
    {
        var shopper = await repository.TableNoTracking.Where(e => e.Id == dto.ShopperId).SingleOrDefaultAsync();

        if (shopper == null)
        {
            return new ServiceResult(false, ApiResultStatusCode.NotFound, localizer["ShopperNotFound"]);
        }

        bool isAdmin = false;
        var loginUser = await userRepository.GetByIdAsync(cancellationToken, userId);
        if (loginUser != null)
            isAdmin = await userManager.IsInRoleAsync(loginUser, "Admin");

        if (shopper.UserId != userId && !isAdmin)
        {
            return new ServiceResult(false, ApiResultStatusCode.UnAuthorized, localizer["UnAuthorized"]);
        }

        if (dto.FileType != FileType.ShopperMainImage && dto.FileType != FileType.ShopperMainVideo)
        {
            return new ServiceResult(false, ApiResultStatusCode.NotFound, localizer["IncorrectFileType"]);
        }

        var oldMainFile = await fileRelationRepository.Table
            .Where(e => e.EntityTypeName == nameof(Shopper) && e.EntityId == shopper.Id
                                                             && e.File.Type == dto.FileType)
            .Include(e => e.File)
            .SingleOrDefaultAsync();

        if (oldMainFile != null)
        {
            var newFileType = dto.FileType == FileType.ShopperMainImage ? FileType.ShopperImage
                : FileType.ShopperVideo;

            await fileService.UpdateAsync(oldMainFile.File, newFileType, cancellationToken);

            oldMainFile.File.Type = newFileType;
            await fileRelationRepository.UpdateAsync(oldMainFile, cancellationToken);
        }

        var newMainFile = new FileRelation();

        newMainFile = await fileRelationRepository.Table
            .Where(e => e.FileId == dto.FileId && e.EntityTypeName == nameof(Shopper) && e.EntityId == shopper.Id)
            .Include(e => e.File)
            .SingleOrDefaultAsync();

        if (newMainFile != null)
        {
            await fileService.UpdateAsync(newMainFile.File, dto.FileType, cancellationToken);
            newMainFile.File.Type = dto.FileType;
            await fileRelationRepository.UpdateAsync(newMainFile, cancellationToken);
        }
        else
        {
            newMainFile = new FileRelation
            {
                EntityId = shopper.Id,
                EntityTypeName = nameof(Shopper),
                FileId = dto.FileId,
                CreatedAt = DateTime.UtcNow,
                Enabled = true
            };

            await fileRelationRepository.AddAsync(newMainFile, cancellationToken);
        }

        return new ServiceResult(true, ApiResultStatusCode.Success, "");
    }


    public async Task<ServiceResult<List<FileDto>>> GetFilesAsync(int id, CancellationToken cancellationToken)
    {
        var shopper = await repository.TableNoTracking.Where(e => e.Id == id).SingleOrDefaultAsync();

        if (shopper == null)
        {
            return new ServiceResult<List<FileDto>>(false, ApiResultStatusCode.NotFound, null, localizer["ShopperNotFound"]);
        }

        var result = await fileRelationRepository.TableNoTracking
            .Where(e => e.EntityTypeName == nameof(Shopper) && e.EntityId == shopper.Id
            && e.File.Type != FileType.ShopperLicense && e.File.Type != FileType.ShopperDocOrRent)
            .Select(e => new FileDto
            {
                FileId = e.FileId,
                FileType = e.File.Type
            }).ToListAsync();

        foreach (var file in result)
        {
            file.Link = (await fileService.GetLinkAsync(file.FileId.Value, file.FileType.Value, cancellationToken)).Data;
        }

        return new ServiceResult<List<FileDto>>(result);
    }
}