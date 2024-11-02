namespace Domain.Dto.Common;

public class PagedListDto<T>
{
    public List<T> Data { get; set; }
    public int Count { get; set; }
}