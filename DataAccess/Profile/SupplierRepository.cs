using DataAccess.Base;
using Domain.Dto.Common;
using Domain.Dto.Supplier;
using Domain.Entity.Profile;
using Domain.Extensions;
using Domain.Repository.Profile;
using Domain.ViewEntity.Profile;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Profile;

public class SupplierRepository(AppDbContext dbContext) : Repository<Supplier>(dbContext), ISupplierRepository
{
    private readonly DbSet<SupplierListView> _listViewSet = dbContext.Set<SupplierListView>();

    public async Task<PagedListDto<SupplierListDto>> GetListAsync(RequestedPageDto<SupplierListFilterDto> dto, CancellationToken cancellationToken)
    {
        var result = _listViewSet.AsNoTracking();
        if (!string.IsNullOrEmpty(dto.Filter.Name))
        {
            result = result.Where(x => x.Name.Contains(dto.Filter.Name));
        }

        return await result.ToPagedListDtoAsync(dto, x =>
            new SupplierListDto
            {
                Id = x.Id,
                Mobile = x.Mobile,
                Name = x.Name,
                IsActive = x.IsActive,
                NaionalCode = x.NationalId
            }, cancellationToken);
    }

    public async Task<int> GetUserIdBySupplierId(int id, CancellationToken cancellationToken)
    {
        return (await TableNoTracking.FirstOrDefaultAsync(x => x.Id == id, cancellationToken)).UserId;
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

    public Task<Supplier> GetByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return Table.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task<bool> IsNationalIdUniqueAsync(string nationalId, CancellationToken cancellationToken)
    {
        var result = await TableNoTracking.Where(x => x.NationalId == nationalId).CountAsync(cancellationToken);
        return result == 0;
    }

    public async Task<bool> IsCompanyNationalIdUniqueAsync(string companyNationalId, CancellationToken cancellationToken)
    {
        var result = await TableNoTracking.Where(x => x.CompanyNationalId == companyNationalId).CountAsync(cancellationToken);
        return result == 0;
    }

    public async Task<bool> IsMobileUniqueAsync(string mobile, CancellationToken cancellationToken)
    {
        return !(await TableNoTracking.AnyAsync(x => x.Mobile == mobile));
    }
}