using Domain.Dto.Common;

namespace Domain.Service.Place;

public interface IProvinceService
{
    Task<ServiceResult<List<IdTitleDto>>> GetAllAsync(KeywordDto dto, CancellationToken cancellationToken);
}