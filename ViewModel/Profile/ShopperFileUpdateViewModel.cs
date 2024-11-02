using Common.Enums;
using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Profile;

public class ShopperFileUpdateViewModel
{
    [JsonConverter(typeof(ShopperEncryptedJsonConverter))]
    public int ShopperId { get; set; }
    public Guid FileId { get; set; }
    public FileType FileType { get; set; }
}