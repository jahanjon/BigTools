using Domain.Dto.Communication;

namespace Domain.Service;

public interface IMessageSendService
{
    Task<ServiceResult<bool, string>> SendLoginCodeAsync(MessageSendDto dto, CancellationToken cancellationToken);
}