using Domain.Dto.Common;
using Domain.Dto.Product;

namespace Domain.Service.Product;

public interface IPackageTypeService
{
    Task<ServiceResult<bool, string>> CreateAsync(PackageTypeCreateDto dto, CancellationToken cancellationToken);

    Task<ServiceResult<PagedListDto<PackageTypeDto>>> GetListAsync(RequestedPageDto<KeywordDto> dto,
        CancellationToken cancellationToken);

    Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(CancellationToken cancellationToken);


    Task<ServiceResult<bool, string>> UpdateAsync(PackageTypeUpdateDto titleCreateDto,
        CancellationToken cancellationToken);

    Task<ServiceResult<bool, string>> DeleteAsync(int id,
        CancellationToken cancellationToken);
}