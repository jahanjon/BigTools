using AutoMapper;
using Domain.Dto.Common;
using Domain.Dto.Place;
using Domain.Service.Place;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Common;
using ViewModel.Place;

namespace API.Areas.Place.Controllers;

public class CityController(ICityService service, IMapper mapper) : BasePlaceController
{
    [HttpPost]
    [Authorize(Roles = "Admin,Supplier,Shopper")]
    public async Task<IActionResult> GetByProvinceIdAsync(KeywordViewModel<ProvinceIdViewModel> viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<KeywordDto<ProvinceIdDto>>(viewModel);
        var response = await service.GetByProvinceIdAsync(dto, cancellationToken);
        var result = mapper.Map<List<CityKeyValueViewModel>>(response.Data);
        return new AppHttpResponse<List<IdTitleDto>>(response).Create(result);
    }
}