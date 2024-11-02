using Common.Enums;

namespace Domain.Dto.Product;

public class BrandFilterDto
{
    public ActiveStatus ActiveStatus { get; set; }
    public string Name { get; set; }
    public string EnName { get; set; }
}