using Domain.Dto.Common;
using Newtonsoft.Json;
using ViewModel.Category;
using ViewModel.EncryptedJsonConverters.Profile;
using ViewModel.File;
using ViewModel.Place;
using ViewModel.Product;

namespace ViewModel.Profile;

public class ShopperFullViewModel
{
    [JsonConverter(typeof(ShopperEncryptedJsonConverter))]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Phone { get; set; }
    public string PersonName { get; set; }
    public string NationCode { get; set; }
    public DateTime? BirthDate { get; set; }
    public CityKeyValueViewModel HomeCity { get; set; }
    public ProvinceKeyValueViewModel HomeProvince { get; set; }
    public string HomePostalCode { get; set; }
    public string HomeAddress { get; set; }
    public CityKeyValueViewModel City { get; set; }
    public ProvinceKeyValueViewModel Province { get; set; }
    public string PostalCode { get; set; }
    public string Address { get; set; }
    public bool IsRent { get; set; }
    public bool HasLicense { get; set; }
    public bool IsRetail { get; set; }
    public int Area { get; set; }
    public FileViewModel? LicenseImage { get; set; }
    public List<FileViewModel> BannerImages { get; set; }
    public List<FileViewModel> DocOrRentImages { get; set; }
    public List<BrandSummaryViewModel> Brands { get; set; }
    public List<CategoryLevel1KeyValueViewModel> Categories { get; set; }
    public List<ShopperFriendViewModel> Friends { get; set; }
}