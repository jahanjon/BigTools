using Domain.Entity.Shopping;
using Domain.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Shopping
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        Task<ShoppingCart> GetByShopperIdAsync(int shopperId, CancellationToken cancellationToken);
    }
}
