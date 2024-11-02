namespace Domain.Dto.Product;

public class BrandDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string EnName { get; set; }
    public string? OwnerName { get; set; }
    public int? OwnerId { get; set; }
    public Guid Logo { get; set; }
    public string LogoLink { get; set; }
    public bool Enabled { get; set; }
    public DateTime CreatedAt { get; set; }
}