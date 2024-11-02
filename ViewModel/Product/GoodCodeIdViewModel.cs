using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class GoodCodeIdViewModel
{
    [JsonConverter(typeof(GoodCodeEncryptedJsonConverter))]
    public int GoodCodeId { get; set; }
}