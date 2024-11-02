using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class BrandIdViewModel
{
    [JsonConverter(typeof(BrandEncryptedJsonConverter))]
    public int Id { get; set; }
}