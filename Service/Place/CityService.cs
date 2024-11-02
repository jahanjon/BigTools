using Domain;
using Domain.Dto.Common;
using Domain.Dto.Place;
using Domain.Repository.Place;
using Domain.Service.Place;

namespace Service.Place;

public class CityService(ICityRepository repository) : ICityService
{
    public async Task<ServiceResult<List<IdTitleDto>>> GetByProvinceIdAsync(KeywordDto<ProvinceIdDto> dto, CancellationToken cancellationToken)
    {
        var result = await repository.GetByProvinceIdAsync(dto.Keyword, dto.Data.Id, cancellationToken);
        return new ServiceResult<List<IdTitleDto>>(result);
    }
}