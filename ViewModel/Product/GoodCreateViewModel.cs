using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Category;
using ViewModel.EncryptedJsonConverters.Product;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Product;

public class GoodCreateViewModel
{
    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int SupplierId { get; set; }

    public string Name { get; set; }

    [JsonConverter(typeof(CategoryEncryptedJsonConverter))]
    public int CategoryId { get; set; }

    public string SupplierCode { get; set; }

    [JsonConverter(typeof(BrandEncryptedJsonConverter))]
    public int BrandId { get; set; }

    [JsonConverter(typeof(PackageTypeEncryptedJsonConverter))]
    public int PackageTypeId { get; set; }

    public int CountInBox { get; set; }

    [JsonConverter(typeof(UnitEncryptedJsonConverter))]
    public int UnitId { get; set; }

    public decimal Price { get; set; }
    public bool InStock { get; set; }
    public int InStockCount { get; set; }
    public List<Guid> Images { get; set; }
    public string Description { get; set; }
}