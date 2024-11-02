using DataAccess.Base;
using Domain.Entity.Category;
using Domain.Repository.Category;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Category;

public class CategoryLevel1Repository(AppDbContext dbContext) : BaseRepository<int, CategoryLevel1>(dbContext), ICategoryLevel1Repository
{
    public Task<List<CategoryLevel1>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken)
    {
        return Table.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
    }
}