using Common.Enums;

namespace Domain.Dto.Common;

public class RequestedPageDto<TFilter> : RequestedPageDto
{
    public TFilter Filter { get; set; }
}

public class RequestedPageDto
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public OrderType OrderType { get; set; }
    public string OrderPropertyName { get; set; }
}