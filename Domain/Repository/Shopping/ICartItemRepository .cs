using Domain.Entity.Shopping;
using Domain.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Shopping
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<List<CartItem>> GetItemsByCartIdAsync(int cartId, CancellationToken cancellationToken);
        Task<CartItem> GetItemAsync(int cartId, int goodId, CancellationToken cancellationToken);
    }
}
