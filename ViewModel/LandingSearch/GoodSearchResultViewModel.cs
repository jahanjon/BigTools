using Common.Enums;
using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Product;
using ViewModel.EncryptedJsonConverters.Profile;
using ViewModel.File;

namespace ViewModel.LandingSearch;

public class GoodSearchResultViewModel
{
    [JsonConverter(typeof(GoodEncryptedJsonConverter))]
    public int GoodId { get; set; }

    [JsonConverter(typeof(GoodCodeEncryptedJsonConverter))]
    public int GoodCodeId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string SupplierCode { get; set; }

    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int SupplierId { get; set; }

    public string SupplierName { get; set; }
    public decimal Price { get; set; }
    public int CountInBox { get; set; }
    public string PackageType { get; set; }
    public List<GoodDiscountSearchResultViewModel> GoodDiscounts { get; set; }
    public FileViewModel? File { get; set; }
}

public class GoodDiscountSearchResultViewModel
{
    public string Name { get; set; }
    public int? Percent { get; set; }
    public string GiftItem { get; set; }
    public PaymentType? PaymentType { get; set; }
    public int? PaymentDurationDays { get; set; }
}