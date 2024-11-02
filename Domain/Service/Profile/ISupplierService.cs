using Domain.Dto.Common;
using Domain.Dto.File;
using Domain.Dto.Supplier;

namespace Domain.Service.Profile;

public interface ISupplierService
{
    Task<ServiceResult> CreateAsync(SupplierCreateDto dto, int userId, CancellationToken cancellationToken);
    Task<ServiceResult> UpdateAsync(SupplierUpdateDto dto, int userId, CancellationToken cancellationToken);
    Task<ServiceResult<PagedListDto<SupplierListDto>>> GetListAsync(RequestedPageDto<SupplierListFilterDto> dto, CancellationToken cancellationToken);
    Task<ServiceResult> ToggleActivateAsync(SupplierActivateDto dto, CancellationToken cancellationToken);
    Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(string keyword, int userId, CancellationToken cancellationToken);
    Task<ServiceResult<SupplierWithDetailDto>> GetAsync(int id, CancellationToken cancellationToken);
    Task<ServiceResult> AddFileAsync(SupplierFileDto dto, int userId, CancellationToken cancellationToken);
    Task<ServiceResult> RemoveFileAsync(SupplierFileDto dto, int userId, CancellationToken cancellationToken);
    Task<ServiceResult> UpdateMainFileAsync(SupplierUpdateFileDto dto, int userId, CancellationToken cancellationToken);
    Task<ServiceResult<List<FileDto>>> GetFilesAsync(int id, CancellationToken cancellationToken);
}