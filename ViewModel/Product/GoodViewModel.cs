using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;

namespace ViewModel.Product;

public class GoodViewModel
{
    [JsonConverter(typeof(GoodEncryptedJsonConverter))]
    public int Id { get; set; }
    [JsonConverter(typeof(GoodCodeEncryptedJsonConverter))]
    public int GoodCodeId { get; set; }
    public string Name { get; set; }
    public string SupplierCode { get; set; }
    public int CountInBox { get; set; }
    public decimal Price { get; set; }
}