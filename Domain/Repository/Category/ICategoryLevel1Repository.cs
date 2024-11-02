using Domain.Entity.Category;
using Domain.Repository.Base;

namespace Domain.Repository.Category;

public interface ICategoryLevel1Repository : IBaseRepository<int, CategoryLevel1>
{
    Task<List<CategoryLevel1>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken);
}