using AutoMapper;
using Domain.Dto.Common;
using Domain.Dto.File;
using Domain.Dto.Supplier;
using Domain.Service.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Common;
using ViewModel.File;
using ViewModel.Profile;

namespace API.Areas.Profile.Controllers;

public class SupplierController(ISupplierService service, IMapper mapper) : BaseProfileController
{

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync(SupplierCreateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<SupplierCreateDto>(viewModel);
        var response = await service.CreateAsync(dto, UserId, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync([FromBody] SupplierUpdateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<SupplierUpdateDto>(viewModel);

        var response = await service.UpdateAsync(dto, UserId, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetListAsync(RequestedPageViewModel<SupplierListFilterViewModel> viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<RequestedPageDto<SupplierListFilterDto>>(viewModel);

        var response = await service.GetListAsync(dto, cancellationToken);

        var result = mapper.Map<PagedListViewModel<SupplierListViewModel>>(response.Data);

        return new AppHttpResponse<PagedListDto<SupplierListDto>>(response).Create(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ToggleActiveAsync(SupplierActivateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<SupplierActivateDto>(viewModel);

        var response = await service.ToggleActivateAsync(dto, cancellationToken);

        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Supplier")]
    public async Task<IActionResult> GetAllAsync(KeywordViewModel viewModel, CancellationToken cancellationToken)
    {
        var response = await service.GetAllAsync(viewModel.Keyword, UserId, cancellationToken);

        var result = mapper.Map<List<SupplierKeyValueViewModel>>(response.Data);

        return new AppHttpResponse<Dictionary<int, string>>(response).Create(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Shopper")]
    public async Task<IActionResult> GetAsync(SupplierIdViewModel viewModel, CancellationToken cancellationToken)
    {
        var response = await service.GetAsync(viewModel.Id, cancellationToken);
        var result = mapper.Map<SupplierWithDetailViewModel>(response.Data);
        return new AppHttpResponse<SupplierWithDetailDto>(response).Create(result);
    }

    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> AddFileAsync([FromBody] SupplierFileViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<SupplierFileDto>(viewModel);

        var response = await service.AddFileAsync(dto, UserId, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> RemoveFileAsync([FromBody] SupplierFileViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<SupplierFileDto>(viewModel);

        var response = await service.RemoveFileAsync(dto, UserId, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> UpdateMainFileAsync([FromBody] SupplierUpdateFileViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<SupplierUpdateFileDto>(viewModel);

        var response = await service.UpdateMainFileAsync(dto, UserId, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> GetFilesAsync(SupplierIdViewModel viewModel, CancellationToken cancellationToken)
    {
        var response = await service.GetFilesAsync(viewModel.Id, cancellationToken);
        var result = mapper.Map<List<FileViewModel>>(response.Data);
        return new AppHttpResponse<List<FileDto>>(response).Create(result);
    }
}