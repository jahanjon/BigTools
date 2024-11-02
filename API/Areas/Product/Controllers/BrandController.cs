using AutoMapper;
using Domain.Dto.Common;
using Domain.Dto.Product;
using Domain.Service.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Common;
using ViewModel.Product;

namespace API.Areas.Product.Controllers;

public class BrandController : BaseProductController
{
    private readonly IBrandService _brandService;
    private readonly IMapper _mapper;

    public BrandController(IMapper mapper, IBrandService brandService)
    {
        _mapper = mapper;
        _brandService = brandService;
    }

    [HttpPost]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> CreateAsync(BrandCreateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<BrandCreateDto>(viewModel);

        var response = await _brandService.CreateAsync(dto, cancellationToken);

        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Supplier,Shopper")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var response = await _brandService.GetAllAsync(cancellationToken);

        var result = _mapper.Map<List<BrandKeyValueViewModel>>(response.Data.ToList());

        return new AppHttpResponse<Dictionary<int, string>>(response).Create(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Supplier,Shopper")]
    public async Task<IActionResult> GetListAsync(RequestedPageViewModel<BrandFilterViewModel> viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<RequestedPageDto<BrandFilterDto>>(viewModel);

        var response = await _brandService.GetListAsync(dto, cancellationToken);

        var result = _mapper.Map<PagedListViewModel<BrandViewModel>>(response.Data);

        return new AppHttpResponse<PagedListDto<BrandDto>>(response).Create(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Supplier,Shopper")]
    public async Task<IActionResult> GetAsync(BrandIdViewModel viewModel, CancellationToken cancellationToken)
    {
        var response = await _brandService.GetAsync(viewModel.Id, cancellationToken);

        var result = _mapper.Map<BrandWithDetailViewModel>(response.Data);

        return new AppHttpResponse<BrandWithDetailDto>(response).Create(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(BrandUpdateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<BrandUpdateDto>(viewModel);

        var response = await _brandService.UpdateAsync(dto, cancellationToken);

        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ToggleAsync(BrandIdViewModel viewModel, CancellationToken cancellationToken)
    {
        var response = await _brandService.ToggleAsync(viewModel.Id, cancellationToken);

        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(BrandIdViewModel viewModel, CancellationToken cancellationToken)
    {
        var response = await _brandService.DeleteAsync(viewModel.Id, cancellationToken);

        return new AppHttpResponse(response).Create();
    }
}