using Common.Enums;
using Common.Settings;
using Domain;
using Domain.Dto.Communication;
using Domain.Entity.Communication;
using Domain.Enums;
using Domain.Repository.Communication;
using Domain.Repository.Identity;
using Domain.Service;
using Kavenegar;
using Kavenegar.Core.Exceptions;
using Kavenegar.Core.Models.Enums;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Service;

public class MessageSendService(
    IMessageRepository repository,
    IUserRepository userRepository,
    IOptionsMonitor<SMSSettings> options,
    IStringLocalizer<MessageSendService> localizer)
    : IMessageSendService
{
    private readonly SMSSettings _settings = options.CurrentValue;

    public async Task<ServiceResult<bool, string>> SendLoginCodeAsync(MessageSendDto dto, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(cancellationToken, dto.UserId);

        var message = new Message
        {
            User = user,
            Content = dto.Content,
            Enabled = true,
            PlatformType = PlatformType.SMS,
            ContentType = MessageContentType.LoginCode,
            CreatedAt = DateTime.UtcNow,
            UserId = dto.UserId,
            Status = MessageStatus.Schulded
        };
        if (_settings.Enable)
        {
            try
            {
                if (!_settings.DebugMode)
                {
                    KavenegarApi api = new(_settings.Token);

                    var result = await api.VerifyLookup(user.Mobile, $"{dto.Content}", "bigtools-login", VerifyLookupType.Sms);

                    message.Status = (MessageStatus)result.Status;
                    message.Content = result.Message;
                    message.MessageBackId = result.Messageid;
                }

                await repository.AddAsync(message, cancellationToken);

                return new ServiceResult<bool, string>(localizer["MessageSend"]);
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                throw new Exception("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                throw new Exception("Message : " + ex.Message);
            }
        }

        await repository.AddAsync(message, cancellationToken);

        return new ServiceResult<bool, string>(errors: localizer["MessageServiceIsDisabled"]);
    }
}