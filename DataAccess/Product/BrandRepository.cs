using System.Linq.Dynamic.Core;
using Common.Enums;
using Common.Utilities;
using DataAccess.Base;
using Domain.Dto.Common;
using Domain.Dto.Product;
using Domain.Entity.Product;
using Domain.Repository.Product;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Product;

public class BrandRepository : BaseRepository<int, Brand>, IBrandRepository
{
    public BrandRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PagedListDto<BrandDto>> GetListAsync(RequestedPageDto<BrandFilterDto> dto,
        CancellationToken cancellationToken)
    {
        var result = TableNoTracking;
        if (!string.IsNullOrEmpty(dto.Filter.Name))
        {
            result = result.Where(x => x.Name.Contains(dto.Filter.Name));
        }

        if (!string.IsNullOrEmpty(dto.Filter.EnName))
        {
            result = result.Where(x => x.EnName.Contains(dto.Filter.EnName));
        }

        if (dto.Filter.ActiveStatus != ActiveStatus.All)
        {
            result = result.Where(x => x.Enabled == dto.Filter.ActiveStatus.GetActiveStatus());
        }

        result = string.IsNullOrEmpty(dto.OrderPropertyName)
            ? result.OrderBy(x => x.Id)
            : (IQueryable<Brand>)result.OrderBy($"{dto.OrderPropertyName} {dto.OrderType}");
        var count = await result.CountAsync(cancellationToken);
        var data = await result.Include(x => x.Owner).Skip(dto.PageSize * (dto.PageIndex - 1)).Take(dto.PageSize).Select(x => new BrandDto
        {
            Name = x.Name,
            CreatedAt = x.CreatedAt,
            Id = x.Id,
            Logo = x.LogoFileGuid,
            Enabled = x.Enabled,
            EnName = x.EnName,
            OwnerName = x.Owner.Name,
            OwnerId = x.Owner.Id
        }).ToListAsync(cancellationToken);
        return new PagedListDto<BrandDto>
        {
            Data = data,
            Count = count
        };
    }

    public Task<Dictionary<int, string>> GetAllAsync(CancellationToken cancellationToken)
    {
        return TableNoTracking.ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);
    }
}