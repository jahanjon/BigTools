using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Category;

namespace ViewModel.Category;

public class CategoryUpdateViewModel
{
    [JsonConverter(typeof(CategoryEncryptedJsonConverter))]
    public int Id { get; set; }

    public string Code { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
}