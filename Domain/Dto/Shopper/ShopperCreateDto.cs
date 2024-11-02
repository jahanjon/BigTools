namespace Domain.Dto.Shopper;

public class ShopperCreateDto
{
    public string Name { get; set; }
    public string PersonName { get; set; }
    public string Phone { get; set; }
    public string NationCode { get; set; }
    public List<int> CategoryIds { get; set; }
    public List<int> BrandIds { get; set; }
    public int CityId { get; set; }
    public string PostalCode { get; set; }
    public string Address { get; set; }
    public bool IsRent { get; set; }
    public bool HasLicense { get; set; }
    public bool IsRetail { get; set; }
    public int Area { get; set; }
    public Guid? LicenseImage { get; set; }
    //public Guid? BannerImage { get; set; }
    public Guid? DocOrRentImage { get; set; }
    public List<ShopperFriendDto> Friends { get; set; }
}