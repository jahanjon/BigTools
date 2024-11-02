using Common.Enums;

namespace Domain.Dto.Financial;

public class GoodDiscountDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string ConditionDescription { get; set; }
    public SaleType SaleType { get; set; }
    public PaymentType PaymentType { get; set; }
    public bool HasValueLimit { get; set; }
    public bool HasCostLimit { get; set; }
    public string DiscountResult { get; set; }
}