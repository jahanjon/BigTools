using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class BrandSummaryViewModel
{
    [JsonConverter(typeof(BrandEncryptedJsonConverter))]
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid LogoFileGuid { get; set; }
    public string LogoFileLink { get; set; }
}