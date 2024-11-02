namespace Domain.Dto.Product;

public class GoodDto
{
    public int Id { get; set; }
    public int GoodCodeId { get; set; }
    public string Name { get; set; }
    public string SupplierCode { get; set; }
    public int CountInBox { get; set; }
    public decimal Price { get; set; }
}