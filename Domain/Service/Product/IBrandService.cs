using Common.Enums;
using Domain.Dto.Common;
using Domain.Dto.Product;

namespace Domain.Service.Product;

public interface IBrandService
{
    Task<ServiceResult> CreateAsync(BrandCreateDto dto, CancellationToken cancellationToken);
    Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(CancellationToken cancellationToken);
    Task<ServiceResult<PagedListDto<BrandDto>>> GetListAsync(RequestedPageDto<BrandFilterDto> dto, CancellationToken cancellationToken);
    Task<ServiceResult<string>> GetDownloadLinkAsync(Guid id, FileType type, CancellationToken cancellationToken);
    Task<ServiceResult<BrandWithDetailDto>> GetAsync(int id, CancellationToken cancellationToken);
    Task<ServiceResult> UpdateAsync(BrandUpdateDto dto, CancellationToken cancellationToken);
    Task<ServiceResult> ToggleAsync(int id, CancellationToken cancellationToken);
    Task<ServiceResult> DeleteAsync(int id, CancellationToken cancellationToken);
}