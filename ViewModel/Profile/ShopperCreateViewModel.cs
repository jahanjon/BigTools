using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Category;
using ViewModel.EncryptedJsonConverters.Place;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Profile;

public class ShopperCreateViewModel
{
    public string Name { get; set; }
    public string PersonName { get; set; }
    public string Phone { get; set; }
    public string NationCode { get; set; }

    [JsonConverter(typeof(CategoryIdListEncryptedJsonConverter))]
    public List<int> CategoryIds { get; set; }

    [JsonConverter(typeof(BrandIdListEncryptedJsonConverter))]
    public List<int> BrandIds { get; set; }
    [JsonConverter(typeof(CityEncryptedJsonConverter))]
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
    public List<ShopperFriendViewModel> Friends { get; set; }
}