using AutoMapper;
using Domain.Dto.Common;
using Domain.Dto.Product;
using Domain.Service.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Common;
using ViewModel.Product;

namespace API.Areas.Product.Controllers;

public class PackageTypeController : BaseProductController
{
    private readonly IMapper _mapper;
    private readonly IPackageTypeService _service;

    public PackageTypeController(IMapper mapper, IPackageTypeService service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync(PackageTypeCreateViewModel viewModel,
        CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<PackageTypeCreateDto>(viewModel);

        var response = await _service.CreateAsync(dto, cancellationToken);

        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetListAsync(RequestedPageViewModel<KeywordViewModel> viewModel,
        CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<RequestedPageDto<KeywordDto>>(viewModel);

        var response = await _service.GetListAsync(dto, cancellationToken);

        var result = _mapper.Map<PagedListViewModel<PackageTypeViewModel>>(response.Data);

        return new AppHttpResponse<PagedListDto<PackageTypeDto>>(response).Create(result);
    }

    [HttpPost]
    [Authorize(Roles = "Supplier,Shopper")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var response = await _service.GetAllAsync(cancellationToken);

        var result = _mapper.Map<List<PackageTypeKeyValueViewModel>>(response.Data.ToList());

        return new AppHttpResponse<Dictionary<int, string>>(response).Create(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(PackageUpdateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<PackageTypeUpdateDto>(viewModel);

        var response = await _service.UpdateAsync(dto, cancellationToken);

        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(PackageTypeDeleteViewModel viewModel,
        CancellationToken cancellationToken)
    {
        var response = await _service.DeleteAsync(viewModel.Id, cancellationToken);

        return new AppHttpResponse(response).Create();
    }
}