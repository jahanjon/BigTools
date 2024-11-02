namespace Domain.Dto.Category;

public class CategoryCreateDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int? ParentCategoryId { get; set; }
}