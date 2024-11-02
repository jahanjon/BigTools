using Domain.Dto.Common;
using Domain.Entity.Place;
using Domain.Repository.Base;

namespace Domain.Repository.Place;

public interface IProvinceRepository : IBaseRepository<int, Province>
{
    Task<List<IdTitleDto>> GetAllAsync(string keyword, CancellationToken cancellationToken);
}