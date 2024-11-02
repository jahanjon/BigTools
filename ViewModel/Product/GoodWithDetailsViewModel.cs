using Newtonsoft.Json;
using ViewModel.Category;
using ViewModel.EncryptedJsonConverters.Product;
using ViewModel.EncryptedJsonConverters.Profile;
using ViewModel.File;
using ViewModel.Financial;

namespace ViewModel.Product;

public class GoodWithDetailsViewModel
{
    [JsonConverter(typeof(GoodEncryptedJsonConverter))]
    public int GoodId { get; set; }
    [JsonConverter(typeof(GoodCodeEncryptedJsonConverter))]
    public int GoodCodeId { get; set; }
    public string Name { get; set; }
    public BrandSummaryViewModel? Brand { get; set; }
    public CategoryKeyValueViewModel Category { get; set; }
    public string Description { get; set; }
    public string SupplierCode { get; set; }
    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public decimal Price { get; set; }
    public int CountInBox { get; set; }
    public string PackageType { get; set; }
    public string Unit { get; set; }
    public List<GoodDiscountSummaryViewModel> GoodDiscounts { get; set; }
    public List<FileViewModel> Files { get; set; }
}