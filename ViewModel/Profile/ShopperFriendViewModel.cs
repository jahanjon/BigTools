using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Place;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Profile;

public class ShopperFriendViewModel
{
    [JsonConverter(typeof(ShopperFriendEncryptedJsonConverter))]
    public int Id { get; set; }

    public string Name { get; set; }
    public string Mobile { get; set; }
    [JsonConverter(typeof(CityEncryptedJsonConverter))]
    public int CityId { get; set; }
    public string Description { get; set; }
}