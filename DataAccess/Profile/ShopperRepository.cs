using DataAccess.Base;
using Domain.Dto.Common;
using Domain.Dto.Shopper;
using Domain.Entity.Profile;
using Domain.Extensions;
using Domain.Repository.Profile;
using Domain.ViewEntity.Profile;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Profile;

public class ShopperRepository(AppDbContext dbContext) : Repository<Domain.Entity.Profile.Shopper>(dbContext), IShopperRepository
{
    private readonly DbSet<ShopperListView> _listViewSet = dbContext.Set<ShopperListView>();

    public async Task<PagedListDto<ShopperListDto>> GetListAsync(RequestedPageDto<ShopperListFilterDto> dto, CancellationToken cancellationToken)
    {
        var result = _listViewSet.AsNoTracking();
        if (!string.IsNullOrEmpty(dto.Filter.Name))
        {
            result = result.Where(x => x.Name.Contains(dto.Filter.Name));
        }

        return await result.ToPagedListDtoAsync(dto, x =>
            new ShopperListDto
            {
                Id = x.Id,
                Mobile = x.Mobile,
                Name = x.Name,
                IsActive = x.IsActive,
                NaionalCode = x.NationCode
            }, cancellationToken);
    }

    public async Task<int> GetUserIdByShopperId(int id, CancellationToken cancellationToken)
    {
        return (await TableNoTracking.FirstOrDefaultAsync(x => x.Id == id, cancellationToken)).UserId;
    }

    public Task<Shopper> GetWithDetailsAsync(int id, CancellationToken cancellationToken)
    {
        return TableNoTracking
            .Include(x => x.Categories)
            .Include(x => x.Friends)
            .Include(x => x.City).ThenInclude(y => y.Province)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<Dictionary<int, string>> GetAllAsync(string keyword, int userId, bool isAdmin, CancellationToken cancellationToken)
    {
        var result = TableNoTracking;
        if (!isAdmin)
        {
            result = result.Where(x => x.User.Id == userId);
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            result = result.Where(x => x.Name.Contains(keyword));
        }

        return result?/*.Take(10)*/.ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);
    }

    public async Task<bool> IsNationalIdUniqueAsync(string nationalId, CancellationToken cancellationToken)
    {
        var result = await TableNoTracking.Where(x => x.NationCode == nationalId).CountAsync(cancellationToken);
        return result == 0;
    }

    public async Task<bool> IsMobileUniqueAsync(string mobile, CancellationToken cancellationToken)
    {
        return !(await TableNoTracking.AnyAsync(x => x.Mobile == mobile));
    }
}