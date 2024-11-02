namespace Domain.Dto.Product;

public class GoodFilterDto
{
    public string Name { get; set; }
    public int MainCategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public string SupplierCode { get; set; }
}