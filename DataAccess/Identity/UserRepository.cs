using System.Linq.Dynamic.Core;
using Common.Exceptions;
using Common.Utilities;
using DataAccess.Base;
using Domain.Dto.Common;
using Domain.Dto.Identity;
using Domain.Entity.Identity;
using Domain.Repository.Identity;
using Domain.ViewEntity.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Identity;

public class UserRepository(AppDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    private readonly DbSet<UserPositionView> positionViewSet = dbContext.Set<UserPositionView>();

    public Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken)
    {
        return UpdateAsync(user, cancellationToken);
    }

    public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
    {
        user.LastLoginDate = DateTimeOffset.UtcNow;
        return UpdateAsync(user, cancellationToken);
    }

    public Task<bool> IsMobileRegisteredAsync(string mobile, CancellationToken cancellationToken)
    {
        return TableNoTracking.AnyAsync(x => x.Mobile == mobile, cancellationToken);
    }

    public Task<User> GetByMobileAsync(string mobile, CancellationToken cancellationToken)
    {
        return TableNoTracking.FirstAsync(x => x.Mobile == mobile, cancellationToken);
    }

    public async Task<PagedListDto<UserListDto>> GetListAsync(RequestedPageDto<UserFilterDto> dto, CancellationToken cancellationToken)
    {
        var result = TableNoTracking;
        if (!string.IsNullOrEmpty(dto.Filter.Mobile))
        {
            result = result.Where(x => x.Mobile.Contains(dto.Filter.Mobile));
        }

        result = string.IsNullOrEmpty(dto.OrderPropertyName)
            ? result.OrderBy(x => x.Id)
            : (IQueryable<User>)result.OrderBy($"{dto.OrderPropertyName} {dto.OrderType}");
        var count = await result.CountAsync(cancellationToken);
        var data = await result.Skip(dto.PageSize * (dto.PageIndex - 1)).Take(dto.PageSize).Select(x => new UserListDto
        {
            Mobile = x.Mobile,
            Id = x.Id
        }).ToListAsync(cancellationToken);
        return new PagedListDto<UserListDto>
        {
            Data = data,
            Count = count
        };
    }

    public async Task<bool> ToggleActivateAsync(int userId, CancellationToken cancellationToken)
    {
        var user = await Entities.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        var result = !user.IsActive;
        user.IsActive = result;

        await UpdateAsync(user, cancellationToken);

        return result;
    }

    public Task<List<UserPositionView>> GetPositionsAsync(int userId, bool isAdmin, CancellationToken cancellationToken)
    {
        return isAdmin
            ? positionViewSet.ToListAsync(cancellationToken)
            : positionViewSet.Where(x => x.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
    {
        var exists = await TableNoTracking.AnyAsync(p => p.UserName == user.UserName);
        if (exists)
        {
            throw new BadRequestException("نام کاربری تکراری است");
        }

        var passwordHash = SecurityHelper.GetSha256Hash(password);
        user.PasswordHash = passwordHash;
        await base.AddAsync(user, cancellationToken);
    }
}