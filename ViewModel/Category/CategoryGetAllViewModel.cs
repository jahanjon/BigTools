using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Category;

namespace ViewModel.Category;

public class CategoryGetAllViewModel
{
    public int Level { get; set; }

    [JsonConverter(typeof(CategoryEncryptedJsonConverter))]
    public int? ParentCategoryId { get; set; }
}