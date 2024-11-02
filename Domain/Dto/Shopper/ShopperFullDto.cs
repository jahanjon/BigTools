using Domain.Dto.Common;
using Domain.Dto.File;
using Domain.Dto.Product;

namespace Domain.Dto.Shopper;

public class ShopperFullDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Phone { get; set; }
    public string PersonName { get; set; }
    public string NationCode { get; set; }
    public DateTime? BirthDate { get; set; }
    public IdTitleDto HomeCity { get; set; }
    public IdTitleDto HomeProvince { get; set; }
    public string HomePostalCode { get; set; }
    public string HomeAddress { get; set; }
    public IdTitleDto City { get; set; }
    public IdTitleDto Province { get; set; }
    public string PostalCode { get; set; }
    public string Address { get; set; }
    public bool IsRent { get; set; }
    public bool HasLicense { get; set; }
    public bool IsRetail { get; set; }
    public int Area { get; set; }
    public FileDto? LicenseImage { get; set; }
    public List<FileDto> BannerImages { get; set; }
    public List<FileDto> DocOrRentImages { get; set; }
    public List<BrandSummaryDto> Brands { get; set; }
    public List<IdTitleDto> Categories { get; set; }
    public List<ShopperFriendDto> Friends { get; set; }
}