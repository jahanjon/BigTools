using Common.Enums;

namespace Domain.Dto.Financial;

public class GoodDiscountFilterDto
{
    public string? Keyword { get; set; }
    public SaleType? SaleType { get; set; }
    public PaymentType? PaymentType { get; set; }
    public int? ShopperRankLimit { get; set; }
}