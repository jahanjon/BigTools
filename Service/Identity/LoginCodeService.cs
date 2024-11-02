using Common.Enums;
using Domain;
using Domain.Dto.Communication;
using Domain.Dto.Identity;
using Domain.Entity.Identity;
using Domain.Repository.Identity;
using Domain.Service;
using Domain.Service.Identity;
using Microsoft.Extensions.Localization;

namespace Service.Identity;

public class LoginCodeService(
    ILoginCodeRepository repository,
    IUserRepository userRepository,
    IMessageSendService messageSendService,
    IStringLocalizer<LoginCodeService> localizer)
    : ILoginCodeService
{
    public async Task<ServiceResult<bool, string>> SendToMobileAsync(string mobile, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByMobileAsync(mobile, cancellationToken);
        if (user is { IsNewUser: false, IsActive: false })
        {
            return new ServiceResult<bool, string>(errors: localizer["UserIsNotActive"]);
        }

        var hasValidLoginCode = await repository.HasAnyValidCodeByUserIdAsync(user.Id, cancellationToken);

        if (hasValidLoginCode)
        {
            return new ServiceResult<bool, string>(errors: localizer["LoginCodeAlreadySent"]);
        }

        var random = new Random();
        var randomNumber = random.Next(10000, 100000);

        var loginCode = new LoginCode
        {
            UserId = user.Id,
            Code = randomNumber.ToString(),
            Type = LoginCodeType.SMS,
            Enabled = true,
            CreatedAt = DateTime.UtcNow
        };

        var message = new MessageSendDto
        {
            UserId = user.Id,
            Content = randomNumber.ToString(),
            PlatformType = PlatformType.SMS
        };


        var sendResult = await messageSendService.SendLoginCodeAsync(message, cancellationToken);

        await repository.AddAsync(loginCode, cancellationToken);

        return sendResult;
    }

    public async Task<ServiceResult<bool, string>> ValidateAsync(TokenDto dto, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByMobileAsync(dto.Mobile, cancellationToken);
        var loginCode = await repository.GetValidCodeByUserIdAsync(user.Id, cancellationToken);

        if (loginCode.Code != dto.LoginCode)
        {
            return new ServiceResult<bool, string>(errors: localizer["WrongInput"]);
        }

        await repository.DeleteAsync(loginCode, cancellationToken);
        return new ServiceResult<bool, string>(true);
    }
}