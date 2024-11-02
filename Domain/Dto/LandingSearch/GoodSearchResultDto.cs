using Common.Enums;
using Domain.Dto.File;

namespace Domain.Dto.LandingSearch;

public class GoodSearchResultDto
{
    public int GoodId { get; set; }
    public int GoodCodeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SupplierCode { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public decimal Price { get; set; }
    public int CountInBox { get; set; }
    public string PackageType { get; set; }
    public List<GoodDiscountSearchResultDto> GoodDiscounts { get; set; }
    public FileDto? File { get; set; }
}

public class GoodDiscountSearchResultDto
{
    public string Name { get; set; }
    public int? Percent { get; set; }
    public string GiftItem { get; set; }
    public PaymentType? PaymentType { get; set; }
    public int? PaymentDurationDays { get; set; }
}