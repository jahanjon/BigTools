using AutoMapper;
using Domain.Dto.Common;
using Domain.Dto.File;
using Domain.Dto.Shopper;
using Domain.Dto.Supplier;
using Domain.Service.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Common;
using ViewModel.File;
using ViewModel.Profile;

namespace API.Areas.Profile.Controllers;

public class ShopperController(IShopperSevice service, IMapper mapper) : BaseProfileController
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync(ShopperCreateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<ShopperCreateDto>(viewModel);
        var response = await service.CreateAsync(dto, UserId, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Shopper")]
    public async Task<IActionResult> UpdateAsync(ShopperUpdateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<ShopperUpdateDto>(viewModel);
        var response = await service.UpdateAsync(dto, UserId, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetListAsync(RequestedPageViewModel<ShopperListFilterViewModel> viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<RequestedPageDto<ShopperListFilterDto>>(viewModel);

        var response = await service.GetListAsync(dto, cancellationToken);

        var result = mapper.Map<PagedListViewModel<ShopperListViewModel>>(response.Data);

        return new AppHttpResponse<PagedListDto<ShopperListDto>>(response).Create(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ToggleActiveAsync(ShopperIdViewModel viewModel, CancellationToken cancellationToken)
    {
        var response = await service.ToggleActivateAsync(viewModel.Id, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Supplier,Shopper")]
    public async Task<IActionResult> GetAsync(ShopperIdViewModel viewModel, CancellationToken cancellationToken)
    {
        var response = await service.GetAsync(viewModel.Id, UserId, cancellationToken);
        var result = mapper.Map<ShopperFullViewModel>(response.Data);
        return new AppHttpResponse<ShopperFullDto, string>(response).Create(result);
    }

    [HttpPost]
    [Authorize(Roles = "Shopper")]
    public async Task<IActionResult> AddFileAsync([FromBody] ShopperFileViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<ShopperFileDto>(viewModel);

        var response = await service.AddFileAsync(dto, UserId, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Shopper")]
    public async Task<IActionResult> RemoveFileAsync([FromBody] ShopperFileViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<ShopperFileDto>(viewModel);

        var response = await service.RemoveFileAsync(dto, UserId, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Shopper")]
    public async Task<IActionResult> UpdateMainFileAsync([FromBody] ShopperFileUpdateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<ShopperFileUpdateDto>(viewModel);

        var response = await service.UpdateMainFileAsync(dto, UserId, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Shopper")]
    public async Task<IActionResult> GetFilesAsync(ShopperIdViewModel viewModel, CancellationToken cancellationToken)
    {
        var response = await service.GetFilesAsync(viewModel.Id, cancellationToken);
        var result = mapper.Map<List<FileViewModel>>(response.Data);
        return new AppHttpResponse<List<FileDto>>(response).Create(result);
    }

}