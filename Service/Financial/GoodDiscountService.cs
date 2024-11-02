using System.Linq.Dynamic.Core;
using AutoMapper;
using Common.Enums;
using Domain;
using Domain.Dto.Common;
using Domain.Dto.Financial;
using Domain.Entity.Financial;
using Domain.Entity.Product;
using Domain.Repository.Base;
using Domain.Repository.Profile;
using Domain.Service.Financial;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Identity;

namespace Service.Financial;

public class GoodDiscountService : IGoodDiscountService
{
    private readonly IRepository<GoodCode> _goodCodeRepository;
    private readonly IStringLocalizer<GoodDiscountService> _localizer;
    private readonly IMapper _mapper;
    private readonly IRepository<GoodDiscount> _repository;
    private readonly ISupplierRepository _supplierRepository;

    public GoodDiscountService(
        IMapper mapper,
        IRepository<GoodDiscount> repository,
        IRepository<GoodCode> goodCodeRepository,
        ISupplierRepository supplierRepository,
        IStringLocalizer<GoodDiscountService> localizer)
    {
        _mapper = mapper;
        _repository = repository;
        _goodCodeRepository = goodCodeRepository;
        _supplierRepository = supplierRepository;
        _localizer = localizer;
    }

    public async Task<ServiceResult> CreateAsync(GoodDiscountCreateDto dto, int userId, CancellationToken cancellationToken)
    {
        //Validate enteries:
        if (string.IsNullOrEmpty(dto.Name))
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["NameCanNotBeEmpty"]);
        }

        if (dto.PaymentType == PaymentType.Cache && dto.PaymentDurationDays > 0)
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["PaymentTypeIsInCorrect"]);
        }

        if (dto.PaymentType == PaymentType.NonCache && (dto.PaymentDurationDays == null || dto.PaymentDurationDays == 0))
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["PaymentTypeIsInCorrect"]);
        }

        if (dto.InvoiceDiscountPercent > 0 && dto.GoodDiscountPercent > 0)
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["DiscountOptionOnlyOneLimit"]);
        }

        if ((dto.InvoiceDiscountPercent == null || dto.InvoiceDiscountPercent == 0) &&
            (dto.GoodDiscountPercent == null || dto.GoodDiscountPercent == 0) &&
            string.IsNullOrEmpty(dto.GiftItem))
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["DiscountOptionAtLeastOneLimit"]);
        }

        if (!dto.GoodIds.Any())
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["DiscountGoodsAtLeastOneLimit"]);
        }

        var supplier = dto.SupplierId > 0
            ? await _supplierRepository.GetByIdAsync(cancellationToken, dto.SupplierId)
            : await _supplierRepository.GetByUserIdAsync(userId, cancellationToken);
        if (supplier is null)
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, "supplier not found!");
        }

        var goodCodes = new List<GoodCode>();
        foreach (var goodId in dto.GoodIds)
        {
            var goodCode = _goodCodeRepository.Entities
                .Where(gc => gc.GoodId == goodId && gc.SupplierId == supplier.Id).FirstOrDefault();
            if (goodCode is null)
            {
                return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["OneOrMoreGoodsNotFound"]);
            }

            goodCodes.Add(goodCode);
        }

        //todo : generate uniqe code
        var discountCode = "";

        var goodDiscount = new GoodDiscount
        {
            Name = dto.Name,
            Code = discountCode,
            SupplierId = supplier.Id,
            Supplier = supplier,
            ConditionDescription = dto.ConditionDescription,
            Goods = goodCodes,
            SaleType = dto.SaleType,
            PaymentType = dto.PaymentType,
            PaymentDurationDays = dto.PaymentDurationDays,
            AmountMaxLimit = dto.AmountMaxLimit,
            AmountMinLimit = dto.AmountMinLimit,
            CostMinLimit = dto.CostMinLimit,
            ShopperRankLimit = dto.ShopperRankLimit,
            InvoiceDiscountPercent = dto.InvoiceDiscountPercent,
            GoodDiscountPercent = dto.GoodDiscountPercent,
            GiftItem = dto.GiftItem
        };

        await _repository.AddAsync(goodDiscount, cancellationToken);

        if (dto.PaymentType == PaymentType.Cache)
        {
            supplier.Cash = true;
        }

        if (dto.PaymentType == PaymentType.NonCache)
        {
            supplier.Installments = true;
            if (dto.PaymentDurationDays != null && supplier.InstallmentsDays < dto.PaymentDurationDays)
            {
                supplier.InstallmentsDays = dto.PaymentDurationDays.Value;
            }
        }

        await _supplierRepository.UpdateAsync(supplier, cancellationToken);

        return new ServiceResult(_localizer["GoodDiscountCreated"]);
    }

    public async Task<ServiceResult<PagedListDto<GoodDiscountDto>>> GetListAsync(RequestedPageDto<GoodDiscountFilterDto> dto, int userId, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByUserIdAsync(userId, cancellationToken);
        if (supplier is null)
        {
            return new ServiceResult<PagedListDto<GoodDiscountDto>>(false, ApiResultStatusCode.NotFound, null,
                _localizer["SupplierNotFound"]);
        }

        var query = _repository.TableNoTracking
            .Where(gd => gd.SupplierId == supplier.Id);

        if (!string.IsNullOrEmpty(dto.Filter.Keyword))
        {
            query = query.Where(e => e.Name.Contains(dto.Filter.Keyword));
        }

        if (dto.Filter.SaleType != null)
        {
            query = query.Where(e => e.SaleType == dto.Filter.SaleType);
        }

        if (dto.Filter.PaymentType != null)
        {
            query = query.Where(e => e.PaymentType == dto.Filter.PaymentType);
        }

        if (dto.Filter.ShopperRankLimit == 0)
        {
            query = query.Where(e => e.ShopperRankLimit == dto.Filter.ShopperRankLimit);
        }

        var selectResult = query.Select(e => new GoodDiscountDto
        {
            Id = e.Id,
            Name = e.Name,
            Code = e.Code,
            ConditionDescription = e.ConditionDescription,
            SaleType = e.SaleType,
            PaymentType = e.PaymentType,
            HasValueLimit = e.AmountMinLimit != null || e.AmountMaxLimit != null,
            HasCostLimit = e.CostMinLimit != null,
            DiscountResult =
                e.InvoiceDiscountPercent > 0 ? !string.IsNullOrEmpty(e.GiftItem) ? $" {e.InvoiceDiscountPercent} {_localizer["InvoiceDiscountPercent"]} " + $"+ {_localizer["GiftItem"]} {e.GiftItem} " : $"{e.InvoiceDiscountPercent} {_localizer["InvoiceDiscountPercent"]}" :
                e.GoodDiscountPercent > 0 ? !string.IsNullOrEmpty(e.GiftItem) ? $" {e.GoodDiscountPercent} {_localizer["GoodDiscountPercent"]} " + $"+ {_localizer["GiftItem"]} {e.GiftItem} " : $" {e.GoodDiscountPercent} {_localizer["GoodDiscountPercent"]} " :
                $"{_localizer["GiftItem"]} {e.GiftItem}"
        });

        selectResult = string.IsNullOrEmpty(dto.OrderPropertyName)
            ? selectResult.OrderBy(x => x.Id)
            : (IQueryable<GoodDiscountDto>)selectResult.OrderBy($"{dto.OrderPropertyName} {dto.OrderType}");

        var count = await selectResult.CountAsync(cancellationToken);
        var data = await selectResult.Skip(dto.PageSize * (dto.PageIndex - 1)).Take(dto.PageSize).ToListAsync();

        return new ServiceResult<PagedListDto<GoodDiscountDto>>(new PagedListDto<GoodDiscountDto>
        {
            Data = data,
            Count = count
        });
    }
}