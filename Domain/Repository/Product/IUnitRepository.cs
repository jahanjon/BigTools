using Domain.Dto.Common;
using Domain.Dto.Product;
using Domain.Entity.Product;
using Domain.Repository.Base;

namespace Domain.Repository.Product;

public interface IUnitRepository : IBaseRepository<int, Unit>
{
    Task<bool> IsTitleUniqueAsync(int id, string title, CancellationToken cancellationToken);
    Task<PagedListDto<UnitDto>> GetListAsync(RequestedPageDto<KeywordDto> dto, CancellationToken cancellationToken);
    Task<Dictionary<int, string>> GetAllAsync(CancellationToken cancellationToken);
}