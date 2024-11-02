using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class UnitKeyValueViewModel
{
    [JsonConverter(typeof(UnitEncryptedJsonConverter))]
    public int Key { get; set; }

    public string Value { get; set; }
}