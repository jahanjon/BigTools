using AutoMapper;
using Domain.Dto.Common;
using Domain.Dto.Product;
using Domain.Service.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Common;
using ViewModel.Product;

namespace API.Areas.Product.Controllers;

public class GoodController : BaseProductController
{
    private readonly IGoodService _goodService;
    private readonly IMapper _mapper;

    public GoodController(IMapper mapper, IGoodService goodService)
    {
        _mapper = mapper;
        _goodService = goodService;
    }

    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> CreateAsync(GoodCreateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<GoodCreateDto>(viewModel);

        var response = await _goodService.CreateAsync(dto, UserId, cancellationToken);

        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> GetListAsync([FromBody] RequestedPageViewModel<GoodFilterViewModel> viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<RequestedPageDto<GoodFilterDto>>(viewModel);

        var response = await _goodService.GetListAsync(dto, UserId, cancellationToken);

        var result = _mapper.Map<PagedListViewModel<GoodViewModel>>(response.Data);

        return new AppHttpResponse<PagedListDto<GoodDto>>(response).Create(result);
    }

    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _goodService.GetAllAsync(UserId, cancellationToken);

        var result = _mapper.Map<List<GoodKeyValueViewModel>>(response.Data.ToList());

        return new AppHttpResponse<Dictionary<int, string>>(response).Create(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Supplier,Shopper")]
    public async Task<IActionResult> Get(GoodCodeIdViewModel viewModel, CancellationToken cancellationToken)
    {
        var response = await _goodService.GetAsync(viewModel.GoodCodeId, cancellationToken);

        var result = _mapper.Map<GoodWithDetailsViewModel>(response.Data);

        return new AppHttpResponse<GoodWithDetailsDto>(response).Create(result);
    }
}