using Domain;
using Domain.Dto.Common;
using Domain.Repository.Place;
using Domain.Service.Place;

namespace Service.Place;

public class ProvinceService(IProvinceRepository repository) : IProvinceService
{
    public async Task<ServiceResult<List<IdTitleDto>>> GetAllAsync(KeywordDto dto, CancellationToken cancellationToken)
    {
        var result = await repository.GetAllAsync(dto.Keyword, cancellationToken);
        return new ServiceResult<List<IdTitleDto>>(result);
    }
}