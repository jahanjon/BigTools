using DataAccess.Base;
using Domain.Entity.Shopping;
using Domain.Repository.Profile;
using Domain.Repository.Shopping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Shopping
{
    public class ShoppingCartRepository(AppDbContext dbContext) : Repository<Domain.Entity.Shopping.ShoppingCart>(dbContext), IShoppingCartRepository
    {

        public async Task<ShoppingCart> GetByShopperIdAsync(int shopperId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.FirstAsync(cart => cart.ShopperId == shopperId, cancellationToken);
        }
    }
}
