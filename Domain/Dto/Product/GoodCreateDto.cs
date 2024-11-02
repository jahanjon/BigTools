namespace Domain.Dto.Product;

public class GoodCreateDto
{
    public int SupplierId { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public string SupplierCode { get; set; }
    public int BrandId { get; set; }
    public int PackageTypeId { get; set; }
    public int CountInBox { get; set; }
    public int UnitId { get; set; }
    public decimal Price { get; set; }
    public bool InStock { get; set; }
    public int InStockCount { get; set; }
    public List<Guid> Images { get; set; }
    public string Description { get; set; }
}