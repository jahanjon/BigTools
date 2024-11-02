using Common.Enums;

namespace ViewModel.Financial;

public class GoodDiscountFilterViewModel
{
    public string? Keyword { get; set; }
    public SaleType? SaleType { get; set; }
    public PaymentType? PaymentType { get; set; }
    public int? ShopperRankLimit { get; set; }
}