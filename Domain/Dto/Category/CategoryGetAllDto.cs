namespace Domain.Dto.Category;

public class CategoryGetAllDto
{
    public int Level { get; set; }
    public int? ParentCategoryId { get; set; }
}