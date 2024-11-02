using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class GoodKeyValueViewModel
{
    [JsonConverter(typeof(GoodEncryptedJsonConverter))]
    public int Key { get; set; }

    public string Value { get; set; }
}