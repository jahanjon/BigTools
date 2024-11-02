namespace Domain.Dto.Category;

public class CategoryLevel1Dto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public List<CategoryLevel2Dto> ChildCategories { get; set; }
}

public class CategoryLevel2Dto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public List<CategoryLevel3Dto> ChildCategories { get; set; }
}

public class CategoryLevel3Dto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}