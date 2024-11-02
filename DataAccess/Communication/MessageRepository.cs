using DataAccess.Base;
using Domain.Dto.Common;
using Domain.Dto.Communication;
using Domain.Entity.Communication;
using Domain.Extensions;
using Domain.Repository.Communication;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Communication;

public class MessageRepository(AppDbContext dbContext) : Repository<Message>(dbContext), IMessageRepository
{

    public async Task<PagedListDto<Message>> GetListAsync(RequestedPageDto<MessageFilterDto> dto, CancellationToken cancellationToken)
    {
        var result = TableNoTracking;
        if (dto.Filter.Status.HasValue)
        {
            result = result.Where(x => x.Status == dto.Filter.Status);
        }

        if (dto.Filter.FromDate.HasValue)
        {
            result = result.Where(x => x.CreatedAt >= dto.Filter.FromDate.Value);
        }

        if (dto.Filter.ToDate.HasValue)
        {
            result = result.Where(x => x.CreatedAt <= dto.Filter.ToDate.Value);
        }

        result = result.Include(x => x.User);

        return await result.ToPagedListDtoAsync(dto, cancellationToken);
    }
}