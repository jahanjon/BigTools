using Common.Enums;
using Domain.Enums;

namespace Domain.Dto.LandingSearch;

public class GoodSearchDto
{
    public string Keyword { get; set; }
    public int CategoryId { get; set; }
    public int CategoryLevel { get; set; }
    public PaymentType? PaymentType { get; set; }
    public int? PaymentDurationDays { get; set; }
    public int SupplierId { get; set; }
    public SupplyType? SupplyType { get; set; }
}