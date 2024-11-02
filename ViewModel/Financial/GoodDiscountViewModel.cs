using Common.Enums;
using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Financial;

namespace ViewModel.Financial;

public class GoodDiscountViewModel
{
    [JsonConverter(typeof(GoodDiscountEncryptedJsonConverter))]
    public int Id { get; set; }

    public string Name { get; set; }
    public string Code { get; set; }
    public string ConditionDescription { get; set; }
    public SaleType SaleType { get; set; }
    public PaymentType PaymentType { get; set; }
    public bool HasValueLimit { get; set; }
    public bool HasCostLimit { get; set; }
    public string DiscountResult { get; set; }
}