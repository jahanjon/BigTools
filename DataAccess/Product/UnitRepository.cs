using System.Linq.Dynamic.Core;
using DataAccess.Base;
using Domain.Dto.Common;
using Domain.Dto.Product;
using Domain.Entity.Product;
using Domain.Repository.Product;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Product;

public class UnitRepository : BaseRepository<int, Unit>, IUnitRepository
{
    public UnitRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsTitleUniqueAsync(int id, string title, CancellationToken cancellationToken)
    {
        return !await TableNoTracking.AnyAsync(x => x.Id != id && x.Title == title, cancellationToken);
    }

    public async Task<PagedListDto<UnitDto>> GetListAsync(RequestedPageDto<KeywordDto> dto,
        CancellationToken cancellationToken)
    {
        var result = TableNoTracking;
        if (!string.IsNullOrEmpty(dto.Filter.Keyword))
        {
            result = result.Where(x => x.Title.Contains(dto.Filter.Keyword));
        }

        result = string.IsNullOrEmpty(dto.OrderPropertyName)
            ? result.OrderBy(x => x.Id)
            : (IQueryable<Unit>)result.OrderBy($"{dto.OrderPropertyName} {dto.OrderType}");
        var count = await result.CountAsync(cancellationToken);
        var data = await result.Skip(dto.PageSize * (dto.PageIndex - 1)).Take(dto.PageSize).Select(x => new UnitDto
        {
            Title = x.Title,
            CreatedAt = x.CreatedAt,
            Id = x.Id
        }).ToListAsync(cancellationToken);
        return new PagedListDto<UnitDto>
        {
            Data = data,
            Count = count
        };
    }

    public Task<Dictionary<int, string>> GetAllAsync(CancellationToken cancellationToken)
    {
        return TableNoTracking.ToDictionaryAsync(x => x.Id, x => x.Title, cancellationToken);
    }
}