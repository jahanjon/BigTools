using Domain.Dto.Common;
using Domain.Dto.Shopper;
using Domain.Entity.Profile;
using Domain.Repository.Base;

namespace Domain.Repository.Profile;

public interface IShopperRepository : IRepository<Entity.Profile.Shopper>
{
    Task<PagedListDto<ShopperListDto>> GetListAsync(RequestedPageDto<ShopperListFilterDto> dto, CancellationToken cancellationToken);
    Task<int> GetUserIdByShopperId(int id, CancellationToken cancellationToken);
    Task<Shopper> GetWithDetailsAsync(int id, CancellationToken cancellationToken);
    Task<Dictionary<int, string>> GetAllAsync(string keyword, int userId, bool isAdmin, CancellationToken cancellationToken);
    Task<bool> IsNationalIdUniqueAsync(string nationalId, CancellationToken cancellationToken);
    Task<bool> IsMobileUniqueAsync(string mobile, CancellationToken cancellationToken);
}