using Common.Enums;
using Domain.Dto.Shopping;
using Domain.Entity.Shopping;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Shopping
{
    public interface IShoppingService
    {
        Task<ServiceResult> CreateShoppingCartAsync(ShoppingCartCreateDto dto, CancellationToken cancellationToken);
        Task<ServiceResult> AddItemToCartAsync(ShoppingCartAddItemDto dto, CancellationToken cancellationToken);
        Task<ServiceResult> CheckoutAsync(ShoppingCartCheckoutDto dto, CancellationToken cancellationToken);
    }
}
