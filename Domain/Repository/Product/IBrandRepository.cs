using Domain.Dto.Common;
using Domain.Dto.Product;
using Domain.Entity.Product;
using Domain.Repository.Base;

namespace Domain.Repository.Product;

public interface IBrandRepository : IBaseRepository<int, Brand>
{
    Task<PagedListDto<BrandDto>> GetListAsync(RequestedPageDto<BrandFilterDto> dto,
        CancellationToken cancellationToken);

    Task<Dictionary<int, string>> GetAllAsync(CancellationToken cancellationToken);
}