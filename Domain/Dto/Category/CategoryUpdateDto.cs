namespace Domain.Dto.Category;

public class CategoryUpdateDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
}