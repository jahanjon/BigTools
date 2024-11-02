using AutoMapper;
using Domain.Dto.Common;
using Domain.Dto.Financial;
using Domain.Service.Financial;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Common;
using ViewModel.Financial;

namespace API.Areas.Financial.Controllers;

public class GoodDiscountController : BaseFinancialController
{
    private readonly IGoodDiscountService _goodDiscountService;
    private readonly IMapper _mapper;

    public GoodDiscountController(IMapper mapper, IGoodDiscountService goodDiscountService)
    {
        _mapper = mapper;
        _goodDiscountService = goodDiscountService;
    }

    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> CreateAsync(GoodDiscountCreateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<GoodDiscountCreateDto>(viewModel);
        var response = await _goodDiscountService.CreateAsync(dto, UserId, cancellationToken);
        return new AppHttpResponse(response).Create();
    }


    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> GetListAsync([FromBody] RequestedPageViewModel<GoodDiscountFilterViewModel> viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<RequestedPageDto<GoodDiscountFilterDto>>(viewModel);

        var response = await _goodDiscountService.GetListAsync(dto, UserId, cancellationToken);

        var result = _mapper.Map<PagedListViewModel<GoodDiscountViewModel>>(response.Data);

        return new AppHttpResponse<PagedListDto<GoodDiscountDto>>(response).Create(result);
    }
}