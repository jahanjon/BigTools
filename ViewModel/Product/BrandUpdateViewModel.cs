using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class BrandUpdateViewModel : BrandCreateViewModel
{
    [JsonConverter(typeof(BrandEncryptedJsonConverter))]
    public int Id { get; set; }
}