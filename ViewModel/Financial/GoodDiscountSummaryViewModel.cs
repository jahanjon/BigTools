using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Financial;

namespace ViewModel.Financial;

public class GoodDiscountSummaryViewModel
{
    [JsonConverter(typeof(GoodDiscountEncryptedJsonConverter))]
    public int Id { get; set; }
    public string Name { get; set; }
    public string DiscountResult { get; set; }
}