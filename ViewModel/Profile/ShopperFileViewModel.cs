using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Profile;

public class ShopperFileViewModel
{
    [JsonConverter(typeof(ShopperEncryptedJsonConverter))]
    public int ShopperId { get; set; }
    public List<Guid> FileIds { get; set; }
}