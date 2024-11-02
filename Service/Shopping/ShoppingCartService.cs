using Common.Enums;
using Domain.Entity.Shopping;
using Domain.Repository.Identity;
using Domain.Service.Shopping;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;
using AutoMapper;
using Domain.Entity.Product;
using Domain.Repository.Base;
using Domain.Repository.Shopping;
using Domain.Dto.Shopping;

namespace Service.Shopping
{
    public class ShoppingService : IShoppingService
    {
        private readonly IShoppingCartRepository cartRepository;
        private readonly ICartItemRepository itemRepository;

        public ShoppingService(IShoppingCartRepository cartRepository, ICartItemRepository itemRepository)
        {
            cartRepository = cartRepository;
            itemRepository = itemRepository;
        }

        public async Task<ServiceResult> CreateShoppingCartAsync(ShoppingCartCreateDto dto, CancellationToken cancellationToken)
        {

            var cart = new ShoppingCart
            {
                ShopperId = dto.ShopperId,
                PaymentMethod = dto.PaymentType,
                CreatedAt = DateTime.UtcNow
            };

            await cartRepository.AddAsync(cart, cancellationToken);
            return new ServiceResult( "Shopping cart created successfully.");
        }

        public async Task<ServiceResult> AddItemToCartAsync(ShoppingCartAddItemDto dto, CancellationToken cancellationToken)
        {

            var item = new CartItem
            {
                Id = dto.CartId,
                GoodId = dto.GoodId,
                Quantity = dto.Quantity
            };

            await itemRepository.AddAsync(item, cancellationToken);
            return new ServiceResult( "Item added to cart successfully.");
        }

        public async Task<ServiceResult> CheckoutAsync(ShoppingCartCheckoutDto dto, CancellationToken cancellationToken)
        {
            var cart = await cartRepository.GetByIdAsync(cancellationToken);
            if (cart == null)
            {
                return new ServiceResult( "Shopping cart not found.");
            }


            return new ServiceResult( "Checkout successful.");
        }
    }
}
