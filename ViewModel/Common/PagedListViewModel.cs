namespace ViewModel.Common;

public class PagedListViewModel<T>
{
    public List<T> Data { get; set; }
    public int Count { get; set; }
}