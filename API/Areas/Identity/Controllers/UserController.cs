using AutoMapper;
using Domain.Dto.Common;
using Domain.Dto.Identity;
using Domain.Entity.Identity;
using Domain.Service.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Common;
using ViewModel.Identity;

namespace API.Areas.Identity.Controllers;

public class UserController(IMapper mapper, IUserService userService) : BaseIdentityController
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(UserCreateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<UserCreateDto>(viewModel);

        var result = await userService.CreateAsync(dto, cancellationToken);

        return new AppHttpResponse(result).Create();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterOrLoginAsync(RegisterOrLoginViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<RegisterOrLoginDto>(viewModel);
        var result = await userService.RegisterOrLoginAsync(dto, cancellationToken);
        return new AppHttpResponse<bool, string>(result).Create(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> TokenAsync(TokenViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<TokenDto>(viewModel);
        var response = await userService.GetTokenAsync(dto, cancellationToken);
        var result = mapper.Map<AccessTokenViewModel>(response.Data);
        return new AppHttpResponse<AccessToken, List<string>>(response).Create(result);
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<LoginDto>(viewModel);

        var response = await userService.LoginAsync(dto, cancellationToken);

        return new AppHttpResponse<AccessToken, string>(response).Create(response);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetListAsync(RequestedPageViewModel<UserFilterViewModel> viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<RequestedPageDto<UserFilterDto>>(viewModel);

        var response = await userService.GetListAsync(dto, cancellationToken);

        var result = mapper.Map<PagedListViewModel<UserListViewModel>>(response.Data);

        return new AppHttpResponse<PagedListDto<UserListDto>>(response).Create(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> GetAllPositionAsync(CancellationToken cancellationToken)
    {
        var response = await userService.GetAllPositionAsync(UserId, cancellationToken);
        var result = mapper.Map<UserPositionViewModel>(response.Data);
        return new AppHttpResponse<UserPositionDto>(response).Create(result);
    }
}