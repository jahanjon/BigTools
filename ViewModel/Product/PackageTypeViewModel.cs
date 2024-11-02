using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class PackageTypeViewModel
{
    [JsonConverter(typeof(PackageTypeEncryptedJsonConverter))]
    public int Id { get; set; }

    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
}