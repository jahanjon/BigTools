using Domain.Dto.Common;
using Domain.Dto.Communication;
using Domain.Entity.Communication;

namespace Domain.Service.Communication;

public interface IMessageService
{
    Task<ServiceResult<PagedListDto<Message>>> GetListAsync(RequestedPageDto<MessageFilterDto> dto, CancellationToken cancellationToken);
}