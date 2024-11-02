using System.Linq.Dynamic.Core;
using AutoMapper;
using Common.Enums;
using Domain;
using Domain.Dto.Category;
using Domain.Dto.Common;
using Domain.Dto.File;
using Domain.Dto.Financial;
using Domain.Dto.Product;
using Domain.Entity.Category;
using Domain.Entity.File;
using Domain.Entity.Financial;
using Domain.Entity.Product;
using Domain.Extensions;
using Domain.Repository.Base;
using Domain.Repository.Product;
using Domain.Repository.Profile;
using Domain.Service;
using Domain.Service.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Service.Product;

public class GoodService : IGoodService
{
    private readonly IRepository<Brand> _brandRepository;
    private readonly IRepository<CategoryLevel1> _categoryLevel1Repository;
    private readonly IRepository<CategoryLevel2> _categoryLevel2Repository;
    private readonly IRepository<CategoryLevel3> _categoryLevel3Repository;
    private readonly IFileService _fileService;
    private readonly IRepository<GoodCode> _goodCodeRepository;
    private readonly IRepository<GoodDiscount> _goodDiscountRepository;
    private readonly IStringLocalizer<GoodService> _localizer;
    private readonly IMapper _mapper;
    private readonly IPackageTypeRepository _packageTypeRepository;
    private readonly IRepository<Price> _priceRepository;
    private readonly IRepository<Good> _repository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly IRepository<FileRelation> fileRelationRepository;
    private readonly IStringLocalizerFactory localizerFactory;

    public GoodService(IMapper mapper, IRepository<Good> repository, IRepository<Brand> brandRepository,
        IRepository<CategoryLevel1> categoryLevel1Repository,
        IRepository<CategoryLevel2> categoryLevel2Repository,
        IRepository<CategoryLevel3> categoryLevel3Repository,
        IRepository<GoodCode> goodCodeRepository,
        ISupplierRepository supplierRepository,
        IUnitRepository unitRepository,
        IPackageTypeRepository packageTypeRepository,
        IRepository<Price> priceRepository,
        IStringLocalizer<GoodService> localizer,
        IStringLocalizerFactory localizerFactory,
        IRepository<FileRelation> fileRelationRepository,
        IFileService fileService,
        IRepository<GoodDiscount> goodDiscountRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _brandRepository = brandRepository;
        _categoryLevel1Repository = categoryLevel1Repository;
        _categoryLevel2Repository = categoryLevel2Repository;
        _categoryLevel3Repository = categoryLevel3Repository;
        _goodCodeRepository = goodCodeRepository;
        _supplierRepository = supplierRepository;
        _unitRepository = unitRepository;
        _packageTypeRepository = packageTypeRepository;
        _priceRepository = priceRepository;
        _localizer = localizer;
        this.localizerFactory = localizerFactory;
        this.fileRelationRepository = fileRelationRepository;
        _fileService = fileService;
        _goodDiscountRepository = goodDiscountRepository;
    }

    public async Task<ServiceResult> CreateAsync(GoodCreateDto dto, int userId, CancellationToken cancellationToken)
    {
        var barnd = await _brandRepository.GetByIdAsync(cancellationToken, dto.BrandId);
        var unit = await _unitRepository.GetByIdAsync(cancellationToken, dto.UnitId);
        var packageType = await _packageTypeRepository.GetByIdAsync(cancellationToken, dto.PackageTypeId);
        var categoryL3 = await _categoryLevel3Repository.GetByIdAsync(cancellationToken, dto.CategoryId);
        var categoryL2 = await _categoryLevel2Repository.GetByIdAsync(cancellationToken, categoryL3.ParentCategoryId);
        var categoryL1 = await _categoryLevel1Repository.GetByIdAsync(cancellationToken, categoryL2.ParentCategoryId);

        if (categoryL3 is null)
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["CategoryNotFound"]);
        }

        var code = $"{categoryL1.Code}{categoryL2.Code}{categoryL3.Code}";

        var newGoodSystemCode = await _repository.TableNoTracking.OrderByDescending(x => x.Id).FirstOrDefaultAsync(cancellationToken);
        var newGood = new Good
        {
            Name = dto.Name,
            Brand = barnd,
            BrandId = dto.BrandId > 0 ? dto.BrandId : null,
            CategoryId = dto.CategoryId,
            Category = categoryL3,
            SystemCode = $"{code}{newGoodSystemCode?.Id + 1}",
            CreatedAt = DateTime.UtcNow,
            Description = dto.Description,
            Enabled = true,
            UnitId = dto.UnitId,
            Unit = unit,
            PackageTypeId = dto.PackageTypeId,
            PackageType = packageType,
            CountInBox = dto.CountInBox
        };
        await _repository.AddAsync(newGood, cancellationToken);

        var files = dto.Images.Select(x => new FileRelation
        {
            Enabled = true,
            CreatedAt = DateTime.UtcNow,
            EntityId = newGood.Id,
            EntityTypeName = nameof(Good),
            FileId = x
        });

        await fileRelationRepository.AddRangeAsync(files, cancellationToken);

        var supplier = dto.SupplierId == 0
            ? await _supplierRepository.GetByUserIdAsync(userId, cancellationToken)
            : await _supplierRepository.GetByIdAsync(cancellationToken, dto.SupplierId);
        if (supplier == null)
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["SupplierNotFound"]);
        }

        if (!string.IsNullOrEmpty(dto.SupplierCode))
        {
            var newGoodCode = new GoodCode
            {
                Code = dto.SupplierCode,
                Good = newGood,
                SupplierId = supplier.Id,
                Supplier = supplier,
                GoodId = newGood.Id
            };
            await _goodCodeRepository.AddAsync(newGoodCode, cancellationToken);
        }

        if (dto.Price > 0)
        {
            var newPrice = new Price
            {
                Good = newGood,
                SupplierId = supplier.Id,
                Supplier = supplier,
                GoodId = newGood.Id,
                CreatedAt = DateTime.UtcNow,
                Enabled = true,
                Count = dto.InStockCount,
                Amount = dto.Price
            };

            await _priceRepository.AddAsync(newPrice, cancellationToken);
        }


        return new ServiceResult(_localizer["GoodCreated"]);
    }


    public async Task<ServiceResult<PagedListDto<GoodDto>>> GetListAsync(RequestedPageDto<GoodFilterDto> dto, int userId, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.TableNoTracking.Where(x => x.UserId == userId).FirstOrDefaultAsync();

        IQueryable<Good> query = _repository.TableNoTracking
            .Where(g => g.GoodCodes.Select(gc => gc.SupplierId).ToList().Contains(supplier.Id))
            .Include(g => g.GoodCodes)
            .Where(g => g.Prices.Where(p => p.Enabled).Select(p => p.SupplierId).ToList().Contains(supplier.Id))
            .Include(g => g.Prices);


        if (!string.IsNullOrEmpty(dto.Filter.SupplierCode))
        {
            query = query.Where(g => g.GoodCodes.Any(gc => gc.Code == dto.Filter.SupplierCode));
        }

        if (!string.IsNullOrEmpty(dto.Filter.Name))
        {
            query = query.Where(g => g.Name.Contains(dto.Filter.Name));
        }

        if (dto.Filter.SubCategoryId > 0)
        {
            query = query.Where(g => g.CategoryId == dto.Filter.SubCategoryId);
        }

        var selectResult = query.Select(g => new GoodDto
        {
            Id = g.Id,
            GoodCodeId = g.GoodCodes.FirstOrDefault().Id,
            Name = g.Name,
            SupplierCode = g.GoodCodes.FirstOrDefault().Code,
            CountInBox = g.CountInBox,
            Price = g.Prices.FirstOrDefault().Amount
        });


        selectResult = string.IsNullOrEmpty(dto.OrderPropertyName)
            ? selectResult.OrderBy(x => x.Id)
            : (IQueryable<GoodDto>)selectResult.OrderBy($"{dto.OrderPropertyName} {dto.OrderType}");

        var count = await selectResult.CountAsync(cancellationToken);
        var data = await selectResult.Skip(dto.PageSize * (dto.PageIndex - 1)).Take(dto.PageSize).ToListAsync();

        return new ServiceResult<PagedListDto<GoodDto>>(new PagedListDto<GoodDto>
        {
            Data = data,
            Count = count
        });
    }


    public async Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(int userId, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.TableNoTracking.Where(x => x.UserId == userId).FirstOrDefaultAsync();

        var result = await _repository.TableNoTracking
            .Where(g => g.GoodCodes.Select(gc => gc.SupplierId).ToList().Contains(supplier.Id))
            .Include(g => g.GoodCodes)
            .ToDictionaryAsync(x => x.Id, x => $"{x.GoodCodes.FirstOrDefault().Code}-{x.Name}");

        return new ServiceResult<Dictionary<int, string>>(result);
    }



    public async Task<ServiceResult<GoodWithDetailsDto>> GetAsync(int goodCodeId, CancellationToken cancellationToken)
    {
        var good = await _goodCodeRepository.TableNoTracking
            .Where(gc => gc.Id == goodCodeId)
            .Select(gc => new GoodWithDetailsDto
            {
                GoodId = gc.Good.Id,
                GoodCodeId = gc.Id,
                Name = gc.Good.Name,
                Brand = gc.Good.Brand != null ? new BrandSummaryDto
                    { Id = gc.Good.BrandId.Value, Name = gc.Good.Brand.Name, LogoFileGuid = gc.Good.Brand.LogoFileGuid } : null,
                Category = new CategoryKeyValueDto
                    { Key = gc.Good.CategoryId, Value = gc.Good.Category.Name },
                Description = gc.Good.Description,
                SupplierCode = gc.Code,
                SupplierId = gc.SupplierId,
                SupplierName = gc.Supplier.Name,
                Price = gc.Good.Prices.FirstOrDefault(p => p.SupplierId == gc.SupplierId && p.Enabled).Amount,
                CountInBox = gc.Good.CountInBox,
                PackageType = gc.Good.PackageType.Title,
                Unit = gc.Good.Unit.Title
                //GoodDiscounts
                //Files
            })
            .SingleOrDefaultAsync();

        if (good is null)
        {
            return new ServiceResult<GoodWithDetailsDto>(
                false, ApiResultStatusCode.NotFound, null, _localizer["GoodWithThisGoodCodeIdNotFound"]);
        }

        //brand logo
        if (good.Brand != null)
        {
            good.Brand.LogoFileLink = (await _fileService.GetLinkAsync(
                good.Brand.LogoFileGuid, FileType.BrandLogo, cancellationToken)).Data;
        }

        //good discounts
        var goodDiscounts = await _goodDiscountRepository.TableNoTracking
            .Where(gd => gd.Goods.Select(g => g.Id).ToList().Contains(good.GoodCodeId))
            .Select(gd => new GoodDiscountSummaryDto
            {
                Id = gd.Id,
                Name = gd.Name,
                DiscountResult = gd.GetDiscountResult(localizerFactory)
            }).ToListAsync();

        good.GoodDiscounts = goodDiscounts;

        //files
        var fileIds = await fileRelationRepository.TableNoTracking
            .Where(e => e.EntityTypeName == nameof(Good) && e.EntityId == good.GoodId)
            .Select(e => e.FileId)
            .ToListAsync();

        var files = new List<FileDto>();

        foreach (var id in fileIds)
        {
            files.Add(new FileDto
            {
                FileId = id,
                Link = (await _fileService.GetLinkAsync(id, FileType.Good, cancellationToken)).Data
            });
        }

        good.Files = files;

        return new ServiceResult<GoodWithDetailsDto>(good);
    }
}