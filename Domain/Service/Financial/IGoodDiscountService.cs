using Domain.Dto.Common;
using Domain.Dto.Financial;

namespace Domain.Service.Financial;

public interface IGoodDiscountService
{
    Task<ServiceResult> CreateAsync(GoodDiscountCreateDto dto, int userId, CancellationToken cancellationToken);

    Task<ServiceResult<PagedListDto<GoodDiscountDto>>> GetListAsync(RequestedPageDto<GoodDiscountFilterDto> dto,
        int userId, CancellationToken cancellationToken);
}