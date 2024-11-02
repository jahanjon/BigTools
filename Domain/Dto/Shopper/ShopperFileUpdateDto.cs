using Common.Enums;

namespace Domain.Dto.Shopper;

public class ShopperFileUpdateDto
{
    public int ShopperId { get; set; }
    public Guid FileId { get; set; }
    public FileType FileType { get; set; }
}