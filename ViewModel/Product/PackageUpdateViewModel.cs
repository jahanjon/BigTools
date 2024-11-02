using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class PackageUpdateViewModel
{
    [JsonConverter(typeof(PackageTypeEncryptedJsonConverter))]
    public int Id { get; set; }

    public string Title { get; set; }
}