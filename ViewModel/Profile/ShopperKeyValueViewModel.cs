using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Profile;

public class ShopperKeyValueViewModel
{
    [JsonConverter(typeof(ShopperEncryptedJsonConverter))]
    public int Key { get; set; }

    public string Value { get; set; }
}