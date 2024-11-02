using Domain.Dto.LandingSearch;

namespace Domain.Service.LandingSearch;

public interface ILandingSearchService
{
    public Task<ServiceResult<List<GoodSearchResultDto>>> SearchGoods(GoodSearchDto dto, CancellationToken cancellationToken);
    public Task<ServiceResult<List<SupplierSearchResultDto>>> SearchSuppliers(SupplierSearchDto dto, CancellationToken cancellationToken);
}