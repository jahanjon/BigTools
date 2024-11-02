using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class PackageTypeKeyValueViewModel
{
    [JsonConverter(typeof(PackageTypeEncryptedJsonConverter))]
    public int Key { get; set; }

    public string Value { get; set; }
}