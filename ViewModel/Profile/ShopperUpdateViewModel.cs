using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Category;
using ViewModel.EncryptedJsonConverters.Place;
using ViewModel.EncryptedJsonConverters.Product;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Profile;

public class ShopperUpdateViewModel
{
    [JsonConverter(typeof(ShopperEncryptedJsonConverter))]
    public int Id { get; set; }
    public string? PersonName { get; set; }
    public string? Phone { get; set; }
    public string? NationCode { get; set; }
    public DateTime? BirthDate { get; set; }
    [JsonConverter(typeof(CityEncryptedJsonConverter))]
    public int? HomeCityId { get; set; }
    public string? HomePostalCode { get; set; }
    public string? HomeAddress { get; set; }
    public string? Name { get; set; }
    [JsonConverter(typeof(CityEncryptedJsonConverter))]
    public int? CityId { get; set; }
    public string? PostalCode { get; set; }
    public string? Address { get; set; }
    public bool? IsRent { get; set; }
    public int? Area { get; set; }
    public bool? HasLicense { get; set; }
    public string? LicenseCode { get; set; }
    public bool? IsRetail { get; set; }
    public Guid? LicenseImage { get; set; }
    public bool DocOrRentImagesEdited { get; set; }
    public List<Guid>? DocOrRentImages { get; set; }
    public bool CategoryIdsEdited { get; set; }
    [JsonConverter(typeof(CategoryIdListEncryptedJsonConverter))]
    public List<int>? CategoryIds { get; set; }
    public bool BrandIdsEdited { get; set; }
    [JsonConverter(typeof(BrandIdListEncryptedJsonConverter))]
    public List<int>? BrandIds { get; set; }

}
