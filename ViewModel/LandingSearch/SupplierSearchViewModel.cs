using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Category;
using ViewModel.EncryptedJsonConverters.Place;

namespace ViewModel.LandingSearch;

public class SupplierSearchViewModel
{
    public string Keyword { get; set; }
    [JsonConverter(typeof(CityEncryptedJsonConverter))]
    public int CityId { get; set; }
    [JsonConverter(typeof(ProvinceEncryptedJsonConverter))]
    public int ProvinceId { get; set; }
    [JsonConverter(typeof(CategoryEncryptedJsonConverter))]
    public int CategoryId { get; set; }
    public bool? IsImporter { get; set; }
    public bool? IsProducer { get; set; }
    public bool? IsSpreader { get; set; }
    public bool? CachePayment { get; set; }
    public bool? InstallmentPayment { get; set; }
    public int PaymentDuration { get; set; }
}