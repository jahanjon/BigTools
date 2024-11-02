using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Category;

namespace ViewModel.Product;

public class GoodFilterViewModel
{
    public string Name { get; set; }
    [JsonConverter(typeof(CategoryEncryptedJsonConverter))]
    public int MainCategoryId { get; set; }
    [JsonConverter(typeof(CategoryEncryptedJsonConverter))]
    public int SubCategoryId { get; set; }
    public string SupplierCode { get; set; }
}