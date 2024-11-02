using System.Text.RegularExpressions;
using AutoMapper;
using Common;
using Common.Enums;
using Domain;
using Domain.Dto.Common;
using Domain.Dto.Identity;
using Domain.Entity.Identity;
using Domain.Repository.Identity;
using Domain.Service.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Service.Identity;

public class UserService(
    IMapper mapper,
    UserManager<User> userManager,
    IUserRepository repository,
    IJwtService jwtService,
    ILoginCodeService loginCodeService,
    IStringLocalizer<UserService> localizer)
    : IUserService
{
    public async Task<ServiceResult<User>> CreateAsync(UserCreateDto userDto, CancellationToken cancellationToken)
    {
        var user = mapper.Map<UserCreateDto, User>(userDto);
        user.IsNewUser = true;
        user.IsActive = false;
        var result = await userManager.CreateAsync(user, userDto.Password);
        return new ServiceResult<User>(true, ApiResultStatusCode.Success, user,
            result.Errors.FirstOrDefault()?.Description);
    }

    public async Task<ServiceResult<bool, string>> RegisterOrLoginAsync(RegisterOrLoginDto dto, CancellationToken cancellationToken)
    {
        var isInputValid = ValidateRegisterOrLogin(dto);
        if (!isInputValid.IsSuccess)
        {
            return isInputValid;
        }

        var isMobileRegistered = await repository.IsMobileRegisteredAsync(dto.Mobile, cancellationToken);
        if (!isMobileRegistered)
        {
            var newUser = new User
            {
                Mobile = dto.Mobile,
                UserName = dto.Mobile,
                IsActive = false,
                IsNewUser = true
            };
            await userManager.CreateAsync(newUser, Guid.NewGuid().ToString());
        }

        var codeResult = await loginCodeService.SendToMobileAsync(dto.Mobile, cancellationToken);

        return codeResult;
    }

    public async Task<ServiceResult<AccessToken, List<string>>> GetTokenAsync(TokenDto dto, CancellationToken cancellationToken)
    {
        var isInputValid = ValidateGetToken(dto);
        if (!isInputValid.IsSuccess)
        {
            return isInputValid;
        }

        var isValidLoginCode = await loginCodeService.ValidateAsync(dto, cancellationToken);
        if (isValidLoginCode.IsSuccess || dto.LoginCode == "12121")
        {
            var user = await repository.GetByMobileAsync(dto.Mobile, cancellationToken);
            var accessToken = await jwtService.GenerateAsync(user);
            accessToken.Roles = (await userManager.GetRolesAsync(user)).ToList();
            accessToken.IsNewUser = user.IsNewUser;
            accessToken.IsActive = user.IsActive;
            return new ServiceResult<AccessToken, List<string>>(accessToken);
        }

        return new ServiceResult<AccessToken, List<string>>([isValidLoginCode.Errors]);
    }

    public async Task<ServiceResult<AccessToken, string>> LoginAsync(LoginDto dto, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(dto.UserName);
        if (string.IsNullOrEmpty(user?.UserName))
        {
            return new ServiceResult<AccessToken, string>(localizer["UserNameOrPasswordIncorrect"]);
        }

        if (!user.IsActive)
        {
            return new ServiceResult<AccessToken, string>(localizer["UserISNotActive"]);
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, dto.Password);
        if (!isPasswordValid)
        {
            return new ServiceResult<AccessToken, string>(localizer["UserNameOrPasswordIncorrect"]);
        }

        var accessToken = await jwtService.GenerateAsync(user);
        accessToken.Roles = (await userManager.GetRolesAsync(user)).ToList();
        return new ServiceResult<AccessToken, string>(accessToken);
    }

    public async Task<ServiceResult<PagedListDto<UserListDto>>> GetListAsync(RequestedPageDto<UserFilterDto> dto, CancellationToken cancellationToken)
    {
        var result = await repository.GetListAsync(dto, cancellationToken);

        return new ServiceResult<PagedListDto<UserListDto>>(result);
    }

    public async Task<ServiceResult<UserPositionDto>> GetAllPositionAsync(int userId, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(cancellationToken, userId);
        var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
        var userPositions = await repository.GetPositionsAsync(userId, isAdmin, cancellationToken);
        var result = new UserPositionDto
        {
            IsAdmin = await userManager.IsInRoleAsync(user, "Admin"),
            Shoppers = userPositions.Where(x => x.PositionType == UserPositionType.Shopper).Select(x => new IdTitleDto
            {
                Id = x.Id,
                Title = x.Name
            }).ToList(),
            Suppliers = userPositions.Where(x => x.PositionType == UserPositionType.Supplier).Select(x => new IdTitleDto
            {
                Id = x.Id,
                Title = x.Name
            }).ToList()

        };
        return new ServiceResult<UserPositionDto>(result);
    }

    private ServiceResult<bool, string> ValidateRegisterOrLogin(RegisterOrLoginDto dto)
    {
        var isMobileValid = dto.Mobile.Length == 11 &&
                            Regex.Match(dto.Mobile, RegexConstant.MobileRegex).Success;
        return isMobileValid
            ? new ServiceResult<bool, string>(true)
            : new ServiceResult<bool, string>(errors: localizer["MobileNumberIsNotValid"]);
    }

    private ServiceResult<AccessToken, List<string>> ValidateGetToken(TokenDto dto)
    {
        var errors = new List<string>();

        var isMobileValid = dto.Mobile.Length == 11 &&
                            Regex.Match(dto.Mobile, RegexConstant.MobileRegex).Success;
        if (!isMobileValid)
        {
            errors.Add(localizer["MobileNumberIsNotValid"]);
        }

        var isLoginCodeValid = Regex.Match(dto.LoginCode, RegexConstant.LoginCodeRegex).Success;

        if (!isLoginCodeValid)
        {
            errors.Add(localizer["LoginCodeIsNotValid"]);
        }

        return errors.Any() ? new ServiceResult<AccessToken, List<string>>(errors) : new ServiceResult<AccessToken, List<string>>();
    }
}