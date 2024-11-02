using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Place;

namespace ViewModel.Place;

public class CityKeyValueViewModel
{
    [JsonConverter(typeof(CityEncryptedJsonConverter))]
    public int Key { get; set; }
    public string Value { get; set; }
}