namespace Domain.Dto.Product;

public class BrandCreateDto
{
    public string Name { get; set; }
    public string EnName { get; set; }
    public int? OwnerId { get; set; }
    public Guid LogoFileGuid { get; set; }
    public Guid? PriceListGuid { get; set; }
}