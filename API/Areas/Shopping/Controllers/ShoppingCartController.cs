using AutoMapper;
using Domain.Dto.Shopping;
using Domain.Service.Shopping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Shopping;

namespace API.Areas.Shopping.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingService _service;
        private readonly IMapper _mapper;

        public ShoppingCartController(IShoppingService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Shopper")]
        public async Task<IActionResult> CreateAsync([FromBody] ShoppingCartCreateViewModel viewModel, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<ShoppingCartCreateDto>(viewModel);
            var response = await _service.CreateShoppingCartAsync(dto, cancellationToken);
            return new AppHttpResponse(response).Create();
        }

        [HttpPost("{cartId}/items")]
        [Authorize(Roles = "Shopper")]
        public async Task<IActionResult> AddItemToCartAsync(int cartId, [FromBody] ShoppingCartAddItemViewModel viewModel, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<ShoppingCartAddItemDto>(viewModel);
            dto.CartId = cartId;
            var response = await _service.AddItemToCartAsync(dto, cancellationToken);
            return new AppHttpResponse(response).Create();
        }

        [HttpPost("checkout")]
        [Authorize(Roles = "Shopper")]
        public async Task<IActionResult> CheckoutAsync([FromBody] ShoppingCartCheckoutViewModel viewModel, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<ShoppingCartCheckoutDto>(viewModel);
            var response = await _service.CheckoutAsync(dto, cancellationToken);
            return new AppHttpResponse(response).Create();
        }
    }
}
