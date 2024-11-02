using DataAccess.Base;
using Domain.Entity.Shopping;
using Domain.Repository.Shopping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Shopping
{
    public class CartItemRepository(AppDbContext dbContext) : Repository<Domain.Entity.Shopping.CartItem>(dbContext), ICartItemRepository
    {

        public async Task<List<CartItem>> GetItemsByCartIdAsync(int cartId, CancellationToken cancellationToken)
        {
            return await TableNoTracking
            .Where(item => item.Id == cartId)
            .ToListAsync(cancellationToken);
        }

        public async Task<CartItem> GetItemAsync(int cartId, int goodId, CancellationToken cancellationToken)
        {
            return await TableNoTracking
                .FirstOrDefaultAsync(item => item.Id == cartId && item.GoodId == goodId, cancellationToken);
        }
    }
}
