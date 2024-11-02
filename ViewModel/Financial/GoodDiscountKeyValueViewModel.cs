using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Financial;

namespace ViewModel.Financial;

public class GoodDiscountKeyValueViewModel
{
    [JsonConverter(typeof(GoodDiscountEncryptedJsonConverter))]
    public int Key { get; set; }
    public string Value { get; set; }
}