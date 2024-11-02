using Common.Enums;
using Domain;
using Domain.Dto.Category;
using Domain.Dto.File;
using Domain.Dto.Financial;
using Domain.Dto.LandingSearch;
using Domain.Entity.Category;
using Domain.Entity.File;
using Domain.Entity.Financial;
using Domain.Entity.Place;
using Domain.Entity.Product;
using Domain.Entity.Profile;
using Domain.Enums;
using Domain.Repository.Base;
using Domain.Repository.Profile;
using Domain.Service;
using Domain.Service.LandingSearch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Identity;

namespace Service.LandingSearch;

public class LandingSearchService : ILandingSearchService
{
    private readonly IRepository<CategoryLevel1> _categoryLevel1Repository;
    private readonly IRepository<CategoryLevel2> _categoryLevel2Repository;
    private readonly IRepository<CategoryLevel3> _categoryLevel3Repository;
    private readonly IRepository<City> _cityRepository;
    private readonly IRepository<GoodDiscount> _goodDiscountRepository;
    private readonly IRepository<Good> _goodRepository;
    private readonly IStringLocalizer<LandingSearchService> _localizer;
    private readonly IRepository<Province> _provinceRepository;
    private readonly IRepository<SupplierBrand> _supplierBrandRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IRepository<FileRelation> _fileRelationRepository;
    private readonly IFileService _fileService;

    public LandingSearchService(
        IRepository<Good> goodRepository,
        IRepository<CategoryLevel1> categoryLevel1Repository,
        IRepository<CategoryLevel2> categoryLevel2Repository,
        IRepository<CategoryLevel3> categoryLevel3Repository,
        IRepository<GoodDiscount> goodDiscountRepository,
        IStringLocalizer<LandingSearchService> localizer,
        ISupplierRepository supplierRepository,
        IRepository<City> cityRepository,
        IRepository<Province> provinceRepository,
        IFileService fileService,
        IRepository<FileRelation> fileRelationRepository,
        IRepository<SupplierBrand> supplierBrandRepository)
    {
        _goodRepository = goodRepository;
        _categoryLevel1Repository = categoryLevel1Repository;
        _categoryLevel2Repository = categoryLevel2Repository;
        _categoryLevel3Repository = categoryLevel3Repository;
        _goodDiscountRepository = goodDiscountRepository;
        _localizer = localizer;
        _supplierRepository = supplierRepository;
        _cityRepository = cityRepository;
        _provinceRepository = provinceRepository;
        _fileService = fileService;
        _fileRelationRepository = fileRelationRepository;
        _supplierBrandRepository = supplierBrandRepository;
    }

    public async Task<ServiceResult<List<GoodSearchResultDto>>> SearchGoods(GoodSearchDto dto, CancellationToken cancellationToken)
    {
        var query = _goodRepository.TableNoTracking
            .Where(g => g.Name.Contains(dto.Keyword));

        if (dto.CategoryId > 0)
        {
            if (dto.CategoryLevel == 1)
            {
                var category = await _categoryLevel1Repository.TableNoTracking
                    .Where(e => e.Id == dto.CategoryId).SingleOrDefaultAsync();

                if (category is null)
                {
                    return new ServiceResult<List<GoodSearchResultDto>>(
                        false, ApiResultStatusCode.BadRequest, null, _localizer["CategoryNotFound"]);
                }

                var leafChildIds = await _categoryLevel2Repository.TableNoTracking
                    .Where(e => e.ParentCategoryId == category.Id)
                    .Include(c => c.ChildCategories)
                    .SelectMany(c => c.ChildCategories.Select(l => l.Id))
                    .ToListAsync();

                query = query.Where(g => leafChildIds.Contains(g.CategoryId));
            }
            else if (dto.CategoryLevel == 2)
            {
                var category = await _categoryLevel2Repository.Entities
                    .Where(e => e.Id == dto.CategoryId).SingleOrDefaultAsync();

                if (category is null)
                {
                    return new ServiceResult<List<GoodSearchResultDto>>(
                        false, ApiResultStatusCode.BadRequest, null, _localizer["CategoryNotFound"]);
                }

                var leafChildIds = await _categoryLevel3Repository.TableNoTracking
                    .Where(e => e.ParentCategoryId == dto.CategoryId)
                    .Select(e => e.Id)
                    .ToListAsync();

                query = query.Where(g => leafChildIds.Contains(g.CategoryId));
            }
            else if (dto.CategoryLevel == 3)
            {
                var category = await _categoryLevel3Repository.Entities
                    .Where(e => e.Id == dto.CategoryId).SingleOrDefaultAsync();

                if (category is null)
                {
                    return new ServiceResult<List<GoodSearchResultDto>>(
                        false, ApiResultStatusCode.BadRequest, null, _localizer["CategoryNotFound"]);
                }

                query = query.Where(g => g.CategoryId == dto.CategoryId);
            }
            else
            {
                return new ServiceResult<List<GoodSearchResultDto>>(
                    false, ApiResultStatusCode.BadRequest, null, _localizer["IncorrectLevel"]);
            }
        }

        query = query.Include(g => g.GoodCodes).ThenInclude(gc => gc.Supplier)
            .Include(g => g.Prices)
            .Include(g => g.PackageType);
        var query2 = query.SelectMany(g => g.GoodCodes);

        if (dto.SupplierId > 0)
        {
            query2 = query2.Where(gc => gc.SupplierId == dto.SupplierId);
        }

        if (dto.SupplyType != null)
        {
            var supplier = await _supplierRepository.TableNoTracking
                .Where(s => s.Id == dto.SupplierId).SingleOrDefaultAsync();

            if (supplier == null)
            {
                return new ServiceResult<List<GoodSearchResultDto>>(
                    false, ApiResultStatusCode.NotFound, null, _localizer["SupplierNotFound"]);
            }

            var importBrandIds = await _supplierBrandRepository.TableNoTracking
                .Where(sb => sb.SupplierId == supplier.Id && sb.Type == SupplyType.Import)
                .Select(sb => sb.BrandId).ToListAsync();

            var produceBrandIds = await _supplierBrandRepository.TableNoTracking
                .Where(sb => sb.SupplierId == supplier.Id && sb.Type == SupplyType.Produce)
                .Select(sb => sb.BrandId).ToListAsync();

            var spreadBrandIds = await _supplierBrandRepository.TableNoTracking
                .Where(sb => sb.SupplierId == supplier.Id && sb.Type == SupplyType.Spreader)
                .Select(sb => sb.BrandId).ToListAsync();

            query2 = query2.Where(gc => gc.SupplierId == supplier.Id);

            if (dto.SupplyType == SupplyType.Import)
            {
                query2 = query2.Where(gc => importBrandIds.Contains(gc.Good.BrandId.Value));
            }
            else if (dto.SupplyType == SupplyType.Produce)
            {
                query2 = query2.Where(gc => produceBrandIds.Contains(gc.Good.BrandId.Value));
            }
            else if (dto.SupplyType == SupplyType.Spreader)
            {
                query2 = query2.Where(gc => !importBrandIds.Contains(gc.Good.BrandId.Value) && !produceBrandIds.Contains(gc.Good.BrandId.Value));
            }
            else
            {
                return new ServiceResult<List<GoodSearchResultDto>>(
                    false, ApiResultStatusCode.BadRequest, null, _localizer["IncorrectSupplyType"]);
            }

        }

        var selectedList = await query2.Select(gc => new GoodSearchResultDto
            {
                GoodId = gc.GoodId,
                GoodCodeId = gc.Id,
                Name = gc.Good.Name,
                Description = gc.Good.Description,
                SupplierCode = gc.Code,
                SupplierId = gc.SupplierId,
                SupplierName = gc.Supplier.Name,
                Price = gc.Good.Prices.Where(p => p.SupplierId == gc.SupplierId && p.Enabled).FirstOrDefault().Amount,
                CountInBox = gc.Good.CountInBox,
                PackageType = gc.Good.PackageType.Title
            })
            .OrderBy(e => e.SupplierId)
            .ToListAsync();

        var RemoveList = new List<GoodSearchResultDto>();

        foreach (var item in selectedList)
        {
            var fileId = (await _fileRelationRepository.TableNoTracking.FirstOrDefaultAsync(x => x.EntityTypeName == nameof(Good) && x.EntityId == item.GoodId))?.FileId;

            if (fileId != null)
            {
                var file = new FileDto
                {
                    FileId = fileId,
                    Link = (await _fileService.GetLinkAsync(fileId.Value, FileType.Good, cancellationToken)).Data
                };
                item.File = file;
            }
            ;

            var itemDiscounts = _goodDiscountRepository.TableNoTracking
                .Include(gd => gd.Goods)
                .Where(gd => gd.SupplierId == item.SupplierId && gd.Goods.Any(x => x.Id == item.GoodCodeId));

            if (dto.PaymentType != null)
            {
                itemDiscounts = itemDiscounts.Where(gd => gd.PaymentType == dto.PaymentType);
                if (dto.PaymentType == PaymentType.NonCache && dto.PaymentDurationDays != null && dto.PaymentDurationDays > 0)
                {
                    itemDiscounts = itemDiscounts.Where(gd => gd.PaymentDurationDays >= dto.PaymentDurationDays);
                }
            }

            var selectedItemDiscounts = await itemDiscounts
                .Select(e => new GoodDiscountSearchResultDto
                {
                    Name = e.Name,
                    Percent = e.InvoiceDiscountPercent > 0 ? e.InvoiceDiscountPercent : e.GoodDiscountPercent,
                    GiftItem = e.GiftItem,
                    PaymentType = e.PaymentType,
                    PaymentDurationDays = e.PaymentDurationDays
                })
                .ToListAsync();

            item.GoodDiscounts = selectedItemDiscounts;

            if (!selectedItemDiscounts.Any())
            {
                RemoveList.Add(item);
            }
        }

        if (dto.PaymentType != null && RemoveList.Any())
        {
            foreach (var item in RemoveList)
            {
                selectedList.Remove(item);
            }
        }

        return new ServiceResult<List<GoodSearchResultDto>>(selectedList);


        //todo : handle image of goods
    }

    public async Task<ServiceResult<List<SupplierSearchResultDto>>> SearchSuppliers(SupplierSearchDto dto, CancellationToken cancellationToken)
    {
        var query = _supplierRepository.TableNoTracking
            .Where(s => string.IsNullOrEmpty(dto.Keyword) || s.Name.Contains(dto.Keyword) || s.LastName.Contains(dto.Keyword));

        if (dto.CategoryId > 0)
        {
            var category = await _categoryLevel1Repository.TableNoTracking
                .Where(e => e.Id == dto.CategoryId).SingleOrDefaultAsync();

            if (category is null)
            {
                return new ServiceResult<List<SupplierSearchResultDto>>(
                    false, ApiResultStatusCode.BadRequest, null, _localizer["CategoryNotFound"]);
            }

            query = query.Where(s => s.Categories.Contains(category));
        }

        if (dto.CityId > 0)
        {
            var city = await _cityRepository.TableNoTracking.Where(c => c.Id == dto.CityId).SingleOrDefaultAsync();

            if (city is null)
            {
                return new ServiceResult<List<SupplierSearchResultDto>>(
                    false, ApiResultStatusCode.NotFound, null, _localizer["CityNotFound"]);
            }

            query = query.Where(s => s.CityId == city.Id);
        }
        else if (dto.ProvinceId > 0)
        {
            var province = await _provinceRepository.TableNoTracking
                .Where(p => p.Id == dto.ProvinceId).Include(p => p.Cities).SingleOrDefaultAsync();

            if (province is null)
            {
                return new ServiceResult<List<SupplierSearchResultDto>>(
                    false, ApiResultStatusCode.NotFound, null, _localizer["ProvinceNotFound"]);
            }

            var cityIds = province.Cities.Select(c => c.Id).ToList();

            query = query.Where(s => cityIds.Contains(s.CityId));
        }

        query = query.Where(s => (dto.IsImporter == null || s.HasImport == dto.IsImporter)
                                 && (dto.IsProducer == null || s.HasProduce == dto.IsProducer)
                                 && (dto.IsSpreader == null || s.HasSpread == dto.IsSpreader));

        query = query.Where(s =>
            (dto.CachePayment == null || s.Cash == dto.CachePayment)
            && (dto.InstallmentPayment == null || s.Installments == dto.InstallmentPayment));

        if (dto.InstallmentPayment != null && dto.InstallmentPayment == true && dto.PaymentDuration > 0)
        {
            query = query.Where(s => s.InstallmentsDays * 30 > dto.PaymentDuration);
        }

        var selectedList = await query.Select(s => new SupplierSearchResultDto
        {
            Id = s.Id,
            Name = s.Name,
            CityName = s.City.Name,
            ProvinceName = s.City.Province.Name,
            Categories = s.Categories.Select(sc => new CategoryKeyValueDto
                { Key = sc.Id, Value = sc.Name }).ToList(),
            IsImporter = s.HasImport,
            IsProducer = s.HasProduce,
            IsSpreader = s.HasSpread
        }).ToListAsync();

        foreach (var item in selectedList)
        {
            var file = await _fileRelationRepository.TableNoTracking.Where(x => x.EntityTypeName == nameof(Supplier) && x.EntityId == item.Id && x.File.Type == FileType.SupplierMainImage)
                .Select(x => new FileDto
                {
                    FileId = x.FileId,
                    FileType = x.File.Type
                }).FirstOrDefaultAsync();

            if (file != null)
            {
                file.Link = (await _fileService.GetLinkAsync(file.FileId.Value, file.FileType.Value, cancellationToken)).Data;
            }

            item.Image = file;

            var goodDiscounts = await _goodDiscountRepository.TableNoTracking
                .Where(gd => gd.SupplierId == item.Id)
                .Select(e => new GoodDiscountKeyValueDto
                    { Key = e.Id, Value = e.Name }).ToListAsync();

            item.GoodDiscounts = goodDiscounts;
        }

        return new ServiceResult<List<SupplierSearchResultDto>>(selectedList);
    }
}