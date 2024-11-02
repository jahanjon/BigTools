using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class PackageTypeDeleteViewModel
{
    [JsonConverter(typeof(PackageTypeEncryptedJsonConverter))]
    public int Id { get; set; }
}