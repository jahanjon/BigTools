using Domain.Dto.Category;
using Domain.Dto.File;
using Domain.Dto.Financial;

namespace Domain.Dto.Product;

public class GoodWithDetailsDto
{
    public int GoodId { get; set; }
    public int GoodCodeId { get; set; }
    public string Name { get; set; }
    public BrandSummaryDto? Brand { get; set; }
    public CategoryKeyValueDto Category { get; set; }
    public string Description { get; set; }
    public string SupplierCode { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public decimal Price { get; set; }
    public int CountInBox { get; set; }
    public string PackageType { get; set; }
    public string Unit { get; set; }
    public List<GoodDiscountSummaryDto> GoodDiscounts { get; set; }
    public List<FileDto> Files { get; set; }
}