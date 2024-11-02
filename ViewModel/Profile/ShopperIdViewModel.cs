using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Profile;

public class ShopperIdViewModel
{
    [JsonConverter(typeof(ShopperEncryptedJsonConverter))]
    public int Id { get; set; }
}