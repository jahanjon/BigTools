using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Product;

public class BrandViewModel
{
    [JsonConverter(typeof(BrandEncryptedJsonConverter))]
    public int Id { get; set; }

    public string Name { get; set; }
    public string EnName { get; set; }
    public string? OwnerName { get; set; }

    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int? OwnerId { get; set; }

    public Guid Logo { get; set; }
    public string LogoLink { get; set; }
    public bool Enabled { get; set; }
}