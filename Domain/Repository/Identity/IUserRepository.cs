using Domain.Dto.Common;
using Domain.Dto.Identity;
using Domain.Entity.Identity;
using Domain.Repository.Base;
using Domain.ViewEntity.Identity;

namespace Domain.Repository.Identity;

public interface IUserRepository : IRepository<User>
{
    Task AddAsync(User user, string password, CancellationToken cancellationToken);
    Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken);
    Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
    Task<bool> IsMobileRegisteredAsync(string mobile, CancellationToken cancellationToken);
    Task<User> GetByMobileAsync(string mobile, CancellationToken cancellationToken);
    Task<PagedListDto<UserListDto>> GetListAsync(RequestedPageDto<UserFilterDto> dto, CancellationToken cancellationToken);
    Task<bool> ToggleActivateAsync(int userId, CancellationToken cancellationToken);
    Task<List<UserPositionView>> GetPositionsAsync(int userId, bool isAdmin, CancellationToken cancellationToken);
}