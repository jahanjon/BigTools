using DataAccess.Base;
using Domain.Dto.Common;
using Domain.Entity.Place;
using Domain.Repository.Place;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Place;

public class ProvinceRepository(AppDbContext dbContext) : BaseRepository<int, Province>(dbContext), IProvinceRepository
{
    public Task<List<IdTitleDto>> GetAllAsync(string keyword, CancellationToken cancellationToken)
    {
        var result = TableNoTracking;

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