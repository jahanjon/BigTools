using Domain.Dto.Common;
using Domain.Dto.Place;

namespace Domain.Service.Place;

public interface ICityService
{
    Task<ServiceResult<List<IdTitleDto>>> GetByProvinceIdAsync(KeywordDto<ProvinceIdDto> dto, CancellationToken cancellationToken);
}