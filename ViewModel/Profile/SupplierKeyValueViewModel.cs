using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Profile;

public class SupplierKeyValueViewModel
{
    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int Key { get; set; }

    public string Value { get; set; }
}