using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Product;

public class BrandWithDetailViewModel
{
    [JsonConverter(typeof(BrandEncryptedJsonConverter))]
    public int Id { get; set; }

    public string Name { get; set; }
    public string EnName { get; set; }

    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int? OwnerId { get; set; }

    public string OwnerName { get; set; }
    public Guid LogoFileGuid { get; set; }
    public Guid? PriceListGuid { get; set; }
    public string LogoFileLink { get; set; }
    public string PriceListLink { get; set; }
}