using Domain.Dto.Common;
using Domain.Dto.Communication;
using Domain.Entity.Communication;
using Domain.Repository.Base;

namespace Domain.Repository.Communication;

public interface IMessageRepository : IRepository<Message>
{
    Task<PagedListDto<Message>> GetListAsync(RequestedPageDto<MessageFilterDto> dto, CancellationToken cancellationToken);
}