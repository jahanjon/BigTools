namespace Domain.Dto.Product;

public class BrandSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid LogoFileGuid { get; set; }
    public string LogoFileLink { get; set; }
}