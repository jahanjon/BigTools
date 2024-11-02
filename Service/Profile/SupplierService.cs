using AutoMapper;
using Common.Enums;
using Domain;
using Domain.Dto.Category;
using Domain.Dto.Common;
using Domain.Dto.File;
using Domain.Dto.Financial;
using Domain.Dto.Product;
using Domain.Dto.Supplier;
using Domain.Entity.Category;
using Domain.Entity.File;
using Domain.Entity.Financial;
using Domain.Entity.Identity;
using Domain.Entity.Place;
using Domain.Entity.Product;
using Domain.Entity.Profile;
using Domain.Enums;
using Domain.Repository.Base;
using Domain.Repository.Identity;
using Domain.Repository.Profile;
using Domain.Service;
using Domain.Service.Profile;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Service.Profile;

public class SupplierService(
    ISupplierRepository repository,
    IUserRepository userRepository,
    IRepository<CategoryLevel1> categoryRepository,
    IRepository<City> cityRepository,
    IRepository<Brand> brandRepository,
    IRepository<SupplierBrand> supplierBrandRepository,
    UserManager<User> userManager,
    IMapper mapper,
    IStringLocalizer<SupplierService> localizer,
    IRepository<GoodDiscount> goodDiscountRepository,
    IRepository<GoodCode> goodCodeRepository,
    IRepository<CategoryLevel3> categoryLevel3Repository,
    IRepository<FileRelation> fileRelationRepository,
    IRepository<Province> provinceRepository,
    IFileService fileService)
    : ISupplierService
{
    public async Task<ServiceResult> CreateAsync(SupplierCreateDto dto, int userId, CancellationToken cancellationToken)
    {
        var validateResponse = await ValidateAsync(dto, userId, cancellationToken);
        if (!validateResponse.IsSuccess)
        {
            return validateResponse;
        }

        var user = await userRepository.Table.FirstAsync(x => x.Id == userId, cancellationToken);
        var city = await cityRepository.Table.FirstAsync(x => x.Id == dto.CityId, cancellationToken);
        var categories = await categoryRepository.Table.Where(x => dto.CategoryIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
        var newSupplier = new Supplier
        {
            Address = dto.Address,
            Name = dto.Name,
            LastName = string.Empty,
            Description = dto.Description,
            Mobile = dto.Mobile,
            ManagerMobile = dto.Mobile,
            CityId = dto.CityId,
            UserId = userId,
            Phone = dto.Phone,
            Phone2 = string.Empty,
            PostalCode = string.Empty,
            User = user,
            City = city,
            Categories = categories,
            AccountantMobile = dto.AccountantMobile,
            CoordinatorMobile = dto.CoordinatorMobile,
            IsPerson = dto.IsPerson,
            NationalId = dto.NationalId,
            CompanyNationalId = string.Empty,
            HasImport = dto.HasImport,
            HasProduce = dto.HasProduce,
            HasSpread = dto.HasSpread,
            ImportDescription = dto.ImportDescription,
            ProduceDescription = dto.ProduceDescription,
            SpreaderDescription = dto.SpreaderDescription,
            Installments = dto.Installments,
            InstallmentsDays = dto.InstallmentsDays,
            Cash = dto.Cash,
            CashDays = dto.CashDays,
            PreOrder = dto.PreOrder,
            Enabled = true,
            CreatedAt = DateTime.UtcNow
        };

        var importBrands = await brandRepository.Table.Where(x => dto.Import.Contains(x.Id))
            .ToListAsync(cancellationToken);
        var spreader = await brandRepository.Table.Where(x => dto.Spreader.Contains(x.Id))
            .ToListAsync(cancellationToken);
        var produce = await brandRepository.Table.Where(x => dto.Produce.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (importBrands.Any())
        {
            newSupplier.HasImport = true;
        }

        if (spreader.Any())
        {
            newSupplier.HasSpread = true;
        }

        if (produce.Any())
        {
            newSupplier.HasProduce = true;
        }

        await repository.AddAsync(newSupplier, cancellationToken);
        await userManager.AddToRoleAsync(user, "Supplier");
        var brands = new List<SupplierBrand>();

        foreach (var brand in importBrands)
        {
            brands.Add(new SupplierBrand
            {
                BrandId = brand.Id,
                Supplier = newSupplier,
                Type = SupplyType.Import,
                Brand = brand,
                SupplierId = newSupplier.Id
            });
        }

        foreach (var brand in produce)
        {
            brands.Add(new SupplierBrand
            {
                BrandId = brand.Id,
                Supplier = newSupplier,
                Type = SupplyType.Produce,
                Brand = brand,
                SupplierId = newSupplier.Id
            });
        }

        foreach (var brand in spreader)
        {
            brands.Add(new SupplierBrand
            {
                BrandId = brand.Id,
                Supplier = newSupplier,
                Type = SupplyType.Spreader,
                Brand = brand,
                SupplierId = newSupplier.Id
            });
        }

        await supplierBrandRepository.AddRangeAsync(brands, cancellationToken);
        user.IsNewUser = false;
        await userRepository.UpdateAsync(user, cancellationToken);

        return new ServiceResult(true, ApiResultStatusCode.Success, "");
    }

    public async Task<ServiceResult<PagedListDto<SupplierListDto>>> GetListAsync(RequestedPageDto<SupplierListFilterDto> dto, CancellationToken cancellationToken)
    {
        var result = await repository.GetListAsync(dto, cancellationToken);

        return new ServiceResult<PagedListDto<SupplierListDto>>(result);
    }

    public async Task<ServiceResult> ToggleActivateAsync(SupplierActivateDto dto, CancellationToken cancellationToken)
    {
        var userId = await repository.GetUserIdBySupplierId(dto.Id, cancellationToken);

        var result = await userRepository.ToggleActivateAsync(userId, cancellationToken);

        var message = result ? localizer["SupplierActivate"] : localizer["SupplierDeactivate"];

        return new ServiceResult(true, ApiResultStatusCode.Success, message);
    }

    public async Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(string keyword, int userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(cancellationToken, userId);
        var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
        var result = await repository.GetAllAsync(keyword, userId, isAdmin, cancellationToken);

        return new ServiceResult<Dictionary<int, string>>(result);
    }

    public async Task<ServiceResult<SupplierWithDetailDto>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var supplier = await repository.TableNoTracking
            .Where(s => s.Id == id).Include(s => s.Categories).SingleOrDefaultAsync();

        if (supplier == null)
        {
            return new ServiceResult<SupplierWithDetailDto>(
                false, ApiResultStatusCode.NotFound, null, localizer["SupplierNotFound"]);
        }

        var city = await cityRepository.TableNoTracking
            .Where(c => c.Id == supplier.CityId).Include(c => c.Province).SingleOrDefaultAsync(cancellationToken);

        var categories = mapper.Map<List<CategoryKeyValueDto>>(supplier.Categories);

        var attachments = await fileRelationRepository.TableNoTracking
            .Where(e => e.EntityTypeName == nameof(Supplier) && e.EntityId == supplier.Id)
            .Select(e => new FileDto
            {
                FileId = e.FileId,
                FileType = e.File.Type
            }).ToListAsync();

        foreach (var file in attachments)
        {
            file.Link = (await fileService.GetLinkAsync(file.FileId.Value, file.FileType.Value, cancellationToken)).Data;
        }

        var user = await userRepository.GetByIdAsync(cancellationToken, supplier.UserId);

        var supplierResult = new SupplierWithDetailDto
        {
            Id = supplier.Id,
            Name = supplier.Name,
            CityId = supplier.CityId,
            CityName = city.Name,
            ProvinceId = city.ProvinceId,
            ProvinceName = city.Province.Name,
            Categories = categories,
            Attachments = attachments,
            IsImporter = supplier.HasImport,
            IsProducer = supplier.HasProduce,
            IsSpreader = supplier.HasSpread,

            Phone = supplier.Phone,
            Phone2 = supplier.Phone2,
            Mobile = supplier.Mobile,
            ManagerMobile = supplier.ManagerMobile,
            AccountantMobile = supplier.AccountantMobile,
            CoordinatorMobile = supplier.CoordinatorMobile,
            NationalId = supplier.NationalId,
            CompanyNationalId = supplier.CompanyNationalId,
            Description = supplier.Description,
            Address = supplier.Address,
            PostalCode = supplier.PostalCode,
            IsPerson = supplier.IsPerson,
            BirthDate = supplier.BirthDate,
            Installments = supplier.Installments,
            Cash = supplier.Cash,
            PreOrder = supplier.PreOrder,
            IsActive = user.IsActive
        };

        //paymentDurationDays
        var paymentDurationDaysList = await goodDiscountRepository.TableNoTracking
            .Where(e => e.SupplierId == supplier.Id && e.PaymentDurationDays.Value > 0).Select(e => e.PaymentDurationDays.Value).ToListAsync();

        supplierResult.PaymentDurationDaysList = paymentDurationDaysList;

        //Import details
        //brands
        var importBrandIds = await supplierBrandRepository.TableNoTracking
            .Where(sb => sb.SupplierId == supplier.Id && sb.Type == SupplyType.Import)
            .Select(sb => sb.BrandId).ToListAsync();

        var importBrands = await brandRepository.TableNoTracking
            .Where(b => importBrandIds.Contains(b.Id)).ToListAsync();

        var mappedImportBrands = mapper.Map<List<BrandSummaryDto>>(importBrands);

        foreach (var item in mappedImportBrands)
        {
            item.LogoFileLink = (await fileService.GetLinkAsync
                (item.LogoFileGuid, FileType.BrandLogo, cancellationToken)).Data;
        }

        //goodDiscounts
        var importGoodDiscounts = await goodDiscountRepository.TableNoTracking
            .Where(gd => gd.SupplierId == supplier.Id)
            .Where(gd => gd.Goods.Any(g => importBrandIds.Contains(g.Good.BrandId.Value)))
            .ToListAsync();

        //var mappedImportGoodDiscounts = _mapper.Map<List<GoodDiscountSummaryDto>>(importGoodDiscounts);
        var mappedImportGoodDiscounts = importGoodDiscounts.Select(e => new GoodDiscountSummaryDto
        {
            Id = e.Id,
            Name = e.Name,
            DiscountResult = e.InvoiceDiscountPercent > 0 ? !string.IsNullOrEmpty(e.GiftItem) ? $" {e.InvoiceDiscountPercent} {localizer["InvoiceDiscountPercent"]} " + $"+ {localizer["GiftItem"]} {e.GiftItem} " : $"{e.InvoiceDiscountPercent} {localizer["InvoiceDiscountPercent"]}" :
                e.GoodDiscountPercent > 0 ? !string.IsNullOrEmpty(e.GiftItem) ? $" {e.GoodDiscountPercent} {localizer["GoodDiscountPercent"]} " + $"+ {localizer["GiftItem"]} {e.GiftItem} " : $" {e.GoodDiscountPercent} {localizer["GoodDiscountPercent"]} " :
                $"{localizer["GiftItem"]} {e.GiftItem}"
        }).ToList();

        //categories
        var importCategoryLevel3Ids = await goodCodeRepository.TableNoTracking
            .Where(gc => gc.SupplierId == supplier.Id)
            .Where(gc => importBrandIds.Contains(gc.Good.BrandId.Value))
            .Select(gc => gc.Good.CategoryId).ToListAsync();

        var importCategoryLevel2Ids = await categoryLevel3Repository.TableNoTracking
            .Where(c3 => importCategoryLevel3Ids.Contains(c3.Id))
            .Select(c3 => c3.ParentCategoryId).ToListAsync();

        var importCategories = await categoryRepository.TableNoTracking
            .Where(c => c.ChildCategories.Any(e => importCategoryLevel2Ids.Contains(e.Id)))
            .ToListAsync();

        var mappedImportCategories = mapper.Map<List<CategoryKeyValueDto>>(importCategories);

        supplierResult.ImportDetails = new SupplyDetailsDto
        {
            Categories = mappedImportCategories,
            Brands = mappedImportBrands,
            GoodDiscounts = mappedImportGoodDiscounts,
            Description = supplier.ImportDescription
        };

        //Produce details
        //brands
        var produceBrandIds = await supplierBrandRepository.TableNoTracking
            .Where(sb => sb.SupplierId == supplier.Id && sb.Type == SupplyType.Produce)
            .Select(sb => sb.BrandId).ToListAsync();

        var produceBrands = await brandRepository.TableNoTracking
            .Where(b => produceBrandIds.Contains(b.Id)).ToListAsync();

        var mappedProduceBrands = mapper.Map<List<BrandSummaryDto>>(produceBrands);

        foreach (var item in mappedProduceBrands)
        {
            item.LogoFileLink = (await fileService.GetLinkAsync
                (item.LogoFileGuid, FileType.BrandLogo, cancellationToken)).Data;
        }

        //goodDiscounts
        var produceGoodDiscounts = await goodDiscountRepository.TableNoTracking
            .Where(gd => gd.SupplierId == supplier.Id)
            .Where(gd => gd.Goods.Any(g => produceBrandIds.Contains(g.Good.BrandId.Value)))
            .ToListAsync();

        //var mappedProduceGoodDiscounts = _mapper.Map<List<GoodDiscountSummaryDto>>(produceGoodDiscounts);
        var mappedProduceGoodDiscounts = produceGoodDiscounts.Select(e => new GoodDiscountSummaryDto
        {
            Id = e.Id,
            Name = e.Name,
            DiscountResult = e.InvoiceDiscountPercent > 0 ? !string.IsNullOrEmpty(e.GiftItem) ? $" {e.InvoiceDiscountPercent} {localizer["InvoiceDiscountPercent"]} " + $"+ {localizer["GiftItem"]} {e.GiftItem} " : $"{e.InvoiceDiscountPercent} {localizer["InvoiceDiscountPercent"]}" :
                e.GoodDiscountPercent > 0 ? !string.IsNullOrEmpty(e.GiftItem) ? $" {e.GoodDiscountPercent} {localizer["GoodDiscountPercent"]} " + $"+ {localizer["GiftItem"]} {e.GiftItem} " : $" {e.GoodDiscountPercent} {localizer["GoodDiscountPercent"]} " :
                $"{localizer["GiftItem"]} {e.GiftItem}"
        }).ToList();

        //categories
        var produceCategoryLevel3Ids = await goodCodeRepository.TableNoTracking
            .Where(gc => gc.SupplierId == supplier.Id)
            .Where(gc => produceBrandIds.Contains(gc.Good.BrandId.Value))
            .Select(gc => gc.Good.CategoryId).ToListAsync();

        var produceCategoryLevel2Ids = await categoryLevel3Repository.TableNoTracking
            .Where(c3 => produceCategoryLevel3Ids.Contains(c3.Id))
            .Select(c3 => c3.ParentCategoryId).ToListAsync();

        var produceCategories = await categoryRepository.TableNoTracking
            .Where(c => c.ChildCategories.Any(e => produceCategoryLevel2Ids.Contains(e.Id)))
            .ToListAsync();

        var mappedProduceCategories = mapper.Map<List<CategoryKeyValueDto>>(produceCategories);

        supplierResult.ProduceDetails = new SupplyDetailsDto
        {
            Categories = mappedProduceCategories,
            Brands = mappedProduceBrands,
            GoodDiscounts = mappedProduceGoodDiscounts,
            Description = supplier.ProduceDescription
        };

        //Spread details
        //brands
        var spreadBrandIds = await supplierBrandRepository.TableNoTracking
            .Where(sb => sb.SupplierId == supplier.Id && sb.Type == SupplyType.Spreader)
            .Select(sb => sb.BrandId).ToListAsync();

        var spreadBrands = await brandRepository.TableNoTracking
            .Where(b => spreadBrandIds.Contains(b.Id)).ToListAsync();

        var mappedSpreadBrands = mapper.Map<List<BrandSummaryDto>>(spreadBrands);

        foreach (var item in mappedSpreadBrands)
        {
            item.LogoFileLink = (await fileService.GetLinkAsync
                (item.LogoFileGuid, FileType.BrandLogo, cancellationToken)).Data;
        }

        //goodDiscounts
        var spreadGoodDiscounts = await goodDiscountRepository.TableNoTracking
            .Where(gd => gd.SupplierId == supplier.Id)
            .Where(gd => gd.Goods
                .Any(g => !importBrandIds.Contains(g.Good.BrandId.Value) && !produceBrandIds.Contains(g.Good.BrandId.Value)))
            .ToListAsync();

        //var mappedSpreadGoodDiscounts = _mapper.Map<List<GoodDiscountSummaryDto>>(spreadGoodDiscounts);
        var mappedSpreadGoodDiscounts = spreadGoodDiscounts.Select(e => new GoodDiscountSummaryDto
        {
            Id = e.Id,
            Name = e.Name,
            DiscountResult = e.InvoiceDiscountPercent > 0 ? !string.IsNullOrEmpty(e.GiftItem) ? $" {e.InvoiceDiscountPercent} {localizer["InvoiceDiscountPercent"]} " + $"+ {localizer["GiftItem"]} {e.GiftItem} " : $"{e.InvoiceDiscountPercent} {localizer["InvoiceDiscountPercent"]}" :
                e.GoodDiscountPercent > 0 ? !string.IsNullOrEmpty(e.GiftItem) ? $" {e.GoodDiscountPercent} {localizer["GoodDiscountPercent"]} " + $"+ {localizer["GiftItem"]} {e.GiftItem} " : $" {e.GoodDiscountPercent} {localizer["GoodDiscountPercent"]} " :
                $"{localizer["GiftItem"]} {e.GiftItem}"
        }).ToList();

        //categories
        var spreadCategoryLevel3Ids = await goodCodeRepository.TableNoTracking
            .Where(gc => gc.SupplierId == supplier.Id)
            .Where(gc => !importBrandIds.Contains(gc.Good.BrandId.Value) && !produceBrandIds.Contains(gc.Good.BrandId.Value))
            .Select(gc => gc.Good.CategoryId).ToListAsync();

        var spreadCategoryLevel2Ids = await categoryLevel3Repository.TableNoTracking
            .Where(c3 => spreadCategoryLevel3Ids.Contains(c3.Id))
            .Select(c3 => c3.ParentCategoryId).ToListAsync();

        var spreadCategories = await categoryRepository.TableNoTracking
            .Where(c => c.ChildCategories.Any(e => spreadCategoryLevel2Ids.Contains(e.Id)))
            .ToListAsync();

        var mappedSpreadCategories = mapper.Map<List<CategoryKeyValueDto>>(spreadCategories);

        supplierResult.SpreadDetails = new SupplyDetailsDto
        {
            Categories = mappedSpreadCategories,
            Brands = mappedSpreadBrands,
            GoodDiscounts = mappedSpreadGoodDiscounts,
            Description = supplier.SpreaderDescription
        };

        return new ServiceResult<SupplierWithDetailDto>(supplierResult);
    }

    public async Task<ServiceResult> UpdateAsync(SupplierUpdateDto dto, int userId, CancellationToken cancellationToken)
    {
        var supplier = await repository.Table
            .Where(s => s.Id == dto.Id).Include(s => s.Categories).SingleOrDefaultAsync();

        //bool isAdmin = false;
        //var loginUser = await userRepository.GetByIdAsync(cancellationToken, userId);
        //if (loginUser != null)
        //    isAdmin = await userManager.IsInRoleAsync(loginUser, "Admin");

        if (supplier == null)
        {
            return new ServiceResult(false, ApiResultStatusCode.NotFound, localizer["SupplierNotFound"]);
        }

        //if (supplier.UserId != userId && !isAdmin)
        //{
        //    return new ServiceResult(false, ApiResultStatusCode.UnAuthorized, localizer["UnAuthorized"]);
        //}

        if (dto.NationalId != null)
        {
            var isNationalIdUnique = await repository.IsNationalIdUniqueAsync(dto.NationalId, cancellationToken);
            if (!isNationalIdUnique)
            {
                new ServiceResult(false, ApiResultStatusCode.BadRequest, localizer["NationalIDIsNotUnique."]);
            }
        }

        if (dto.CompanyNationalId != null)
        {
            var isCompanyNationalIdUnique = await repository.IsCompanyNationalIdUniqueAsync(dto.CompanyNationalId, cancellationToken);
            if (!isCompanyNationalIdUnique)
            {
                new ServiceResult(false, ApiResultStatusCode.BadRequest, localizer["CompanyNationalIDIsNotUnique."]);
            }
        }

        supplier.Name = dto.Name ?? supplier.Name;
        supplier.Phone = dto.Phone ?? supplier.Phone;
        supplier.Phone2 = dto.Phone2 ?? supplier.Phone2;
        supplier.AccountantMobile = dto.AccountantMobile ?? supplier.AccountantMobile;
        supplier.CoordinatorMobile = dto.CoordinatorMobile ?? supplier.CoordinatorMobile;
        supplier.Description = dto.Description ?? supplier.Description;
        supplier.CityId = dto.CityId ?? supplier.CityId;
        supplier.Address = dto.Address ?? supplier.Address;
        supplier.PostalCode = dto.PostalCode ?? supplier.PostalCode;
        supplier.IsPerson = dto.IsPerson ?? supplier.IsPerson;
        supplier.BirthDate = dto.BirthDate ?? supplier.BirthDate;
        supplier.ImportDescription = dto.ImportDescription ?? supplier.ImportDescription;
        supplier.ProduceDescription = dto.ProduceDescription ?? supplier.ProduceDescription;
        supplier.SpreaderDescription = dto.SpreaderDescription ?? supplier.SpreaderDescription;
        supplier.HasImport = dto.HasImport ?? supplier.HasImport;
        supplier.HasProduce = dto.HasProduce ?? supplier.HasProduce;
        supplier.HasSpread = dto.HasSpread ?? supplier.HasSpread;
        supplier.Installments = dto.Installments ?? supplier.Installments;
        supplier.Cash = dto.Cash ?? supplier.Cash;
        supplier.PreOrder = dto.PreOrder ?? supplier.PreOrder;
        supplier.NationalId = dto.NationalId ?? supplier.NationalId;
        supplier.CompanyNationalId = dto.CompanyNationalId ?? supplier.CompanyNationalId;

        if (dto.CategoryIdsEdited)
        {
            var categories = await categoryRepository.Table.Where(x => dto.CategoryIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            supplier.Categories = categories;
        }

        await repository.UpdateAsync(supplier, cancellationToken);


        var addListBrands = new List<SupplierBrand>();
        var removeListBrands = new List<SupplierBrand>();

        if (dto.ImportBrandsEdited)
        {
            var importBrands = await supplierBrandRepository.Entities
                .Where(e => e.SupplierId == supplier.Id && e.Type == SupplyType.Import).ToListAsync();

            var importBrandIds = importBrands.Select(e => e.BrandId).ToList();

            foreach (var item in dto.ImportBrands)
            {
                if (!importBrandIds.Contains(item))
                {
                    var brand = await brandRepository.GetByIdAsync(cancellationToken, item);
                    if (brand != null)
                    {
                        addListBrands.Add(new SupplierBrand
                        {
                            BrandId = brand.Id,
                            Brand = brand,
                            Supplier = supplier,
                            SupplierId = supplier.Id,
                            Type = SupplyType.Import
                        });
                    }
                }
            }
            foreach (var item in importBrands)
            {
                if (!dto.ImportBrands.Contains(item.BrandId))
                {
                    removeListBrands.Add(item);
                }
            }
        }

        if (dto.ProduceBrandsEdited)
        {
            var produceBrands = await supplierBrandRepository.Entities
                .Where(e => e.SupplierId == supplier.Id && e.Type == SupplyType.Produce).ToListAsync();

            var produceBrandIds = produceBrands.Select(e => e.BrandId).ToList();

            foreach (var item in dto.ProduceBrands)
            {
                if (!produceBrandIds.Contains(item))
                {
                    var brand = await brandRepository.GetByIdAsync(cancellationToken, item);
                    if (brand != null)
                    {
                        addListBrands.Add(new SupplierBrand
                        {
                            BrandId = brand.Id,
                            Brand = brand,
                            Supplier = supplier,
                            SupplierId = supplier.Id,
                            Type = SupplyType.Produce
                        });
                    }
                }
            }
            foreach (var item in produceBrands)
            {
                if (!dto.ProduceBrands.Contains(item.BrandId))
                {
                    removeListBrands.Add(item);
                }
            }
        }

        if (dto.SpreaderBrandsEdited)
        {
            var spreadBrands = await supplierBrandRepository.Entities
                .Where(e => e.SupplierId == supplier.Id && e.Type == SupplyType.Spreader).ToListAsync();

            var spreadBrandIds = spreadBrands.Select(e => e.BrandId).ToList();

            foreach (var item in dto.SpreaderBrands)
            {
                if (!spreadBrandIds.Contains(item))
                {
                    var brand = await brandRepository.GetByIdAsync(cancellationToken, item);
                    if (brand != null)
                    {
                        addListBrands.Add(new SupplierBrand
                        {
                            BrandId = brand.Id,
                            Brand = brand,
                            Supplier = supplier,
                            SupplierId = supplier.Id,
                            Type = SupplyType.Spreader
                        });
                    }
                }
            }
            foreach (var item in spreadBrands)
            {
                if (!dto.SpreaderBrands.Contains(item.BrandId))
                {
                    removeListBrands.Add(item);
                }
            }
        }

        await supplierBrandRepository.AddRangeAsync(addListBrands, cancellationToken);
        await supplierBrandRepository.DeleteRangeAsync(removeListBrands, cancellationToken);

        return new ServiceResult(true, ApiResultStatusCode.Success, localizer["SupplierUpdated"]);
    }

    public async Task<ServiceResult> AddFileAsync(SupplierFileDto dto, int userId, CancellationToken cancellationToken)
    {
        var supplier = await repository.TableNoTracking.Where(e => e.Id == dto.SupplierId).SingleOrDefaultAsync();

        if (supplier == null)
        {
            return new ServiceResult(false, ApiResultStatusCode.NotFound, localizer["SupplierNotFound"]);
        }

        bool isAdmin = false;
        var loginUser = await userRepository.GetByIdAsync(cancellationToken, userId);
        if (loginUser != null)
            isAdmin = await userManager.IsInRoleAsync(loginUser, "Admin");

        if (supplier.UserId != userId && !isAdmin)
        {
            return new ServiceResult(false, ApiResultStatusCode.UnAuthorized, localizer["UnAuthorized"]);
        }

        var files = new List<FileRelation>();
        foreach (var item in dto.FileIds)
        {
            files.Add(new FileRelation
            {
                EntityId = supplier.Id,
                EntityTypeName = nameof(Supplier),
                FileId = item,
                Enabled = true,
                CreatedAt = DateTime.UtcNow
            });
        }

        await fileRelationRepository.AddRangeAsync(files, cancellationToken);

        return new ServiceResult(true, ApiResultStatusCode.Success, "");
    }

    public async Task<ServiceResult> RemoveFileAsync(SupplierFileDto dto, int userId, CancellationToken cancellationToken)
    {
        var supplier = await repository.TableNoTracking.Where(e => e.Id == dto.SupplierId).SingleOrDefaultAsync();

        if (supplier == null)
        {
            return new ServiceResult(false, ApiResultStatusCode.NotFound, localizer["SupplierNotFound"]);
        }

        bool isAdmin = false;
        var loginUser = await userRepository.GetByIdAsync(cancellationToken, userId);
        if (loginUser != null)
            isAdmin = await userManager.IsInRoleAsync(loginUser, "Admin");

        if (supplier.UserId != userId && !isAdmin)
        {
            return new ServiceResult(false, ApiResultStatusCode.UnAuthorized, localizer["UnAuthorized"]);
        }

        var files = await fileRelationRepository.Table
            .Where(e => dto.FileIds.Contains(e.FileId) && e.EntityTypeName == nameof(Supplier) && e.EntityId == supplier.Id)
            .ToListAsync();

        await fileRelationRepository.DeleteRangeAsync(files, cancellationToken);
        return new ServiceResult(true, ApiResultStatusCode.Success, "");
    }

    public async Task<ServiceResult> UpdateMainFileAsync(SupplierUpdateFileDto dto, int userId, CancellationToken cancellationToken)
    {
        var supplier = await repository.TableNoTracking.Where(e => e.Id == dto.SupplierId).SingleOrDefaultAsync();

        if (supplier == null)
        {
            return new ServiceResult(false, ApiResultStatusCode.NotFound, localizer["SupplierNotFound"]);
        }

        bool isAdmin = false;
        var loginUser = await userRepository.GetByIdAsync(cancellationToken, userId);
        if (loginUser != null)
            isAdmin = await userManager.IsInRoleAsync(loginUser, "Admin");

        if (supplier.UserId != userId && !isAdmin)
        {
            return new ServiceResult(false, ApiResultStatusCode.UnAuthorized, localizer["UnAuthorized"]);
        }

        if (dto.FileType != FileType.SupplierMainImage && dto.FileType != FileType.SupplierMainVideo)
        {
            return new ServiceResult(false, ApiResultStatusCode.NotFound, localizer["IncorrectFileType"]);
        }

        var oldMainFile = await fileRelationRepository.Table
            .Where(e => e.EntityTypeName == nameof(Supplier) && e.EntityId == supplier.Id
                                                             && e.File.Type == dto.FileType)
            .Include(e => e.File)
            .SingleOrDefaultAsync();

        if (oldMainFile != null)
        {
            var newFileType = dto.FileType == FileType.SupplierMainImage ? FileType.SupplierImage
                : FileType.SupplierVideo;

            await fileService.UpdateAsync(oldMainFile.File, newFileType, cancellationToken);

            oldMainFile.File.Type = newFileType;
            await fileRelationRepository.UpdateAsync(oldMainFile, cancellationToken);
        }

        var newMainFile = new FileRelation();

        newMainFile = await fileRelationRepository.Table
            .Where(e => e.FileId == dto.FileId && e.EntityTypeName == nameof(Supplier) && e.EntityId == supplier.Id)
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
                EntityId = supplier.Id,
                EntityTypeName = nameof(Supplier),
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
        var supplier = await repository.TableNoTracking.Where(e => e.Id == id).SingleOrDefaultAsync();

        if (supplier == null)
        {
            return new ServiceResult<List<FileDto>>(false, ApiResultStatusCode.NotFound, null, localizer["SupplierNotFound"]);
        }

        var result = await fileRelationRepository.TableNoTracking
            .Where(e => e.EntityTypeName == nameof(Supplier) && e.EntityId == supplier.Id)
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

    private async Task<ServiceResult> ValidateAsync(SupplierCreateDto dto, int userId, CancellationToken cancellationToken)
    {
        if (!await repository.IsMobileUniqueAsync(dto.Mobile, cancellationToken))
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, localizer["MobileIsNotUnique"]);

        if (dto.IsPerson && dto.NationalId.Length != 10)
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, localizer["PersonNationalIdLengthMustBe10Characters"]);
        }

        if (!dto.IsPerson && dto.NationalId?.Length is < 10 or > 11)
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, localizer["LegalNationalIdLengthMustBe10Or11Characters"]);
        }

        var isNationalIdUnique = await repository.IsNationalIdUniqueAsync(dto.NationalId, cancellationToken);
        return !isNationalIdUnique
            ? new ServiceResult(false, ApiResultStatusCode.BadRequest, localizer["NationalIdIsNotUnique"])
            : new ServiceResult();
    }
}