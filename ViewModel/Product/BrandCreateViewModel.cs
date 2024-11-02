using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Product;

public class BrandCreateViewModel
{
    public string Name { get; set; }
    public string EnName { get; set; }

    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int? OwnerId { get; set; }

    public Guid LogoFileGuid { get; set; }
    public Guid? PriceListGuid { get; set; }
}