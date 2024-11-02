using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Category;

public class CategoryLevel1ViewModel
{
    [JsonConverter(typeof(CategoryEncryptedJsonConverter))]
    public int Id { get; set; }

    public string Code { get; set; }
    public string Name { get; set; }
    public List<CategoryLevel2ViewModel> ChildCategories { get; set; }
}


public class CategoryLevel2ViewModel
{
    [JsonConverter(typeof(CategoryEncryptedJsonConverter))]
    public int Id { get; set; }

    public string Code { get; set; }
    public string Name { get; set; }
    public List<CategoryLevel3ViewModel> ChildCategories { get; set; }
}

public class CategoryLevel3ViewModel
{
    [JsonConverter(typeof(CategoryEncryptedJsonConverter))]
    public int Id { get; set; }

    public string Code { get; set; }
    public string Name { get; set; }
}