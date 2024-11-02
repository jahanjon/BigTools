using Common.Enums;

namespace ViewModel.Common;

public class RequestedPageViewModel<TFilter>
{
    public TFilter Filter { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public OrderType OrderType { get; set; }
    public string OrderPropertyName { get; set; }
}