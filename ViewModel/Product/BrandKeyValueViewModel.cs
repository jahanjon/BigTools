using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class BrandKeyValueViewModel
{
    [JsonConverter(typeof(BrandEncryptedJsonConverter))]
    public int Key { get; set; }

    public string Value { get; set; }
}