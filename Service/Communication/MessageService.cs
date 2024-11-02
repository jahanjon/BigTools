using Domain;
using Domain.Dto.Common;
using Domain.Dto.Communication;
using Domain.Entity.Communication;
using Domain.Repository.Communication;
using Domain.Service.Communication;

namespace Service.Communication;

public class MessageService(IMessageRepository repository) : IMessageService
{

    public async Task<ServiceResult<PagedListDto<Message>>> GetListAsync(RequestedPageDto<MessageFilterDto> dto, CancellationToken cancellationToken)
    {
        var result = await repository.GetListAsync(dto, cancellationToken);

        return new ServiceResult<PagedListDto<Message>>(result);
    }
}