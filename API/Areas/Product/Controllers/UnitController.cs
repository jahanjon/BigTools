using AutoMapper;
using Domain.Dto.Common;
using Domain.Dto.Product;
using Domain.Service.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Common;
using ViewModel.Product;

namespace API.Areas.Product.Controllers;

public class UnitController : BaseProductController
{
    private readonly IMapper _mapper;
    private readonly IUnitService _service;

    public UnitController(IMapper mapper, IUnitService service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync(UnitCreateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<UnitCreateDto>(viewModel);

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

        var result = _mapper.Map<PagedListDto<UnitDto>>(response.Data);

        return new AppHttpResponse<PagedListDto<UnitDto>>(response).Create(result);
    }

    [HttpPost]
    [Authorize(Roles = "Supplier,Shopper")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var response = await _service.GetAllAsync(cancellationToken);

        var result = _mapper.Map<List<UnitKeyValueViewModel>>(response.Data.ToList());

        return new AppHttpResponse<Dictionary<int, string>>(response).Create(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(UnitUpdateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<UnitUpdateDto>(viewModel);

        var response = await _service.UpdateAsync(dto, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(UnitDeleteViewModel viewModel, CancellationToken cancellationToken)
    {
        var response = await _service.DeleteAsync(viewModel.Id, cancellationToken);
        return new AppHttpResponse(response).Create();
    }
}