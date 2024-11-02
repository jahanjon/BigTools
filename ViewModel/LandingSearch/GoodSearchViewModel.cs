using Common.Enums;
using Domain.Enums;
using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Category;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.LandingSearch;

public class GoodSearchViewModel
{
    public string Keyword { get; set; }

    [JsonConverter(typeof(CategoryEncryptedJsonConverter))]
    public int CategoryId { get; set; }

    public int CategoryLevel { get; set; }
    public PaymentType? PaymentType { get; set; }
    public int? PaymentDurationDays { get; set; }

    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int SupplierId { get; set; }
    public SupplyType? SupplyType { get; set; }
}