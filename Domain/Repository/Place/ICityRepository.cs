using Domain.Dto.Common;
using Domain.Entity.Place;
using Domain.Repository.Base;

namespace Domain.Repository.Place;

public interface ICityRepository : IBaseRepository<int, City>
{
    Task<List<IdTitleDto>> GetByProvinceIdAsync(string keyword, int data, CancellationToken cancellationToken);
}