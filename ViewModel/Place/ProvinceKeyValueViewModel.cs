using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Place;

namespace ViewModel.Place;

public class ProvinceKeyValueViewModel
{
    [JsonConverter(typeof(ProvinceEncryptedJsonConverter))]
    public int Key { get; set; }
    public string Value { get; set; }
}