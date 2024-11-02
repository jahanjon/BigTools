using Domain.Dto.Common;
using Domain.Dto.Product;

namespace Domain.Service.Product;

public interface IGoodService
{
    Task<ServiceResult> CreateAsync(GoodCreateDto dto, int userId, CancellationToken cancellationToken);

    Task<ServiceResult<PagedListDto<GoodDto>>> GetListAsync(RequestedPageDto<GoodFilterDto> dto, int userId,
        CancellationToken cancellationToken);

    Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(int userId, CancellationToken cancellationToken);
    Task<ServiceResult<GoodWithDetailsDto>> GetAsync(int goodCodeId, CancellationToken cancellationToken);
}