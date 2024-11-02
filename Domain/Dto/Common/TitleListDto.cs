namespace Domain.Dto.Common;

public abstract class TitleListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Enabled { get; set; }
}