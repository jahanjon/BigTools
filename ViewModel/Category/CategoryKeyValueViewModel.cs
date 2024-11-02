using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Category;

namespace ViewModel.Category;

public class CategoryKeyValueViewModel
{
    [JsonConverter(typeof(CategoryEncryptedJsonConverter))]
    public int Key { get; set; }

    public string Value { get; set; }
}