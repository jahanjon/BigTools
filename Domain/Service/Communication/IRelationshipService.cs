using Domain.Dto.Common;
using Domain.Dto.Communication;

namespace Domain.Service.Communication;

public interface IRelationshipService
{
    Task<ServiceResult> CreateAsync(RelationshipCreateDto dto, int userId, CancellationToken cancellationToken);
    Task<ServiceResult> AnswerAsync(RelationshipAnswerDto dto, int userId, CancellationToken cancellationToken);
    Task<ServiceResult<PagedListDto<RelationshipListDto>>> GetListAsync(RequestedPageDto<RelationshipFilterDto> dto, CancellationToken cancellationToken);
}