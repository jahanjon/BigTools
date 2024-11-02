using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Place;

namespace ViewModel.Place;

public class ProvinceIdViewModel
{
    [JsonConverter(typeof(ProvinceEncryptedJsonConverter))]
    public int Id { get; set; }
}