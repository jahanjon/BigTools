namespace Domain.Dto.Product;

public class BrandWithDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string EnName { get; set; }
    public int? OwnerId { get; set; }
    public string? OwnerName { get; set; }
    public Guid LogoFileGuid { get; set; }
    public Guid? PriceListGuid { get; set; }
    public string LogoFileLink { get; set; }
    public string PriceListLink { get; set; }
}