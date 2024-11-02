using Common.Enums;

namespace Domain.Dto.Financial;

public class GoodDiscountCreateDto
{
    public int SupplierId { get; set; }
    public string Name { get; set; }
    public string ConditionDescription { get; set; }
    public ICollection<int> GoodIds { get; set; }
    public SaleType SaleType { get; set; }
    public PaymentType PaymentType { get; set; }
    public int? PaymentDurationDays { get; set; }
    public double? AmountMaxLimit { get; set; }
    public double? AmountMinLimit { get; set; }
    public double? CostMinLimit { get; set; }
    public int? ShopperRankLimit { get; set; }
    public int? InvoiceDiscountPercent { get; set; }
    public int? GoodDiscountPercent { get; set; }
    public string? GiftItem { get; set; }
}