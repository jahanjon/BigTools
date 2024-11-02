using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class UnitDeleteViewModel
{
    [JsonConverter(typeof(UnitEncryptedJsonConverter))]
    public int Id { get; set; }
}