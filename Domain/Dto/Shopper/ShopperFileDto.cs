namespace Domain.Dto.Shopper;

public class ShopperFileDto
{
    public int ShopperId { get; set; }
    public List<Guid> FileIds { get; set; }
}