using DataAccess.Base;
using Domain.Dto.Common;
using Domain.Entity.Place;
using Domain.Repository.Place;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Place;

public class CityRepository(AppDbContext dbContext) : BaseRepository<int, City>(dbContext), ICityRepository
{
    public Task<List<IdTitleDto>> GetByProvinceIdAsync(string keyword, int provinceId, CancellationToken cancellationToken)
    {
        var result = TableNoTracking.Where(x => x.ProvinceId == provinceId);

        if (!string.IsNullOrEmpty(keyword.Trim()))
        {
            result = result.Where(x => x.Name.Contains(keyword));
        }

        return result.Select(x => new IdTitleDto
        {
            Id = x.Id,
            Title = x.Name
        }).ToListAsync(cancellationToken);
    }
}