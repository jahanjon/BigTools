using DataAccess.Base;
using Domain.Entity.Profile;
using Domain.Repository.ShopperDependents;

namespace DataAccess.ShopperDependents;

public class ShopperFriendRepository : Repository<ShopperFriend>
    , IShopperFriendsRepository
{
    public ShopperFriendRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}