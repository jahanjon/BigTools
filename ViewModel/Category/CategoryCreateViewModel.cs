using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Category;

namespace ViewModel.Category;

public class CategoryCreateViewModel
{
    public string Code { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }

    [JsonConverter(typeof(CategoryEncryptedJsonConverter))]
    public int? ParentCategoryId { get; set; }
}