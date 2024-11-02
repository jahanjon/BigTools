using Domain.Dto.Common;
using Domain.Dto.Product;

namespace Domain.Service.Product;

public interface IUnitService
{
    Task<ServiceResult<bool, string>> CreateAsync(UnitCreateDto dto, CancellationToken cancellationToken);

    Task<ServiceResult<PagedListDto<UnitDto>>> GetListAsync(RequestedPageDto<KeywordDto> dto,
        CancellationToken cancellationToken);

    Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(CancellationToken cancellationToken);

    Task<ServiceResult<bool, string>> DeleteAsync(int id, CancellationToken cancellationToken);

    Task<ServiceResult<bool, string>> UpdateAsync(UnitUpdateDto dto, CancellationToken cancellationToken);
}