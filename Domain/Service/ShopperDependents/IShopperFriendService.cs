namespace Domain.Service.ShopperDependents;

public interface IShopperFriendService
{
    Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(CancellationToken cancellationToken);
}