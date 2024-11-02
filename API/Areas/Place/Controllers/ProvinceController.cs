using AutoMapper;
using Domain.Dto.Common;
using Domain.Service.Place;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Common;
using ViewModel.Place;

namespace API.Areas.Place.Controllers;

public class ProvinceController(IProvinceService service, IMapper mapper) : BasePlaceController
{
    [HttpPost]
    [Authorize(Roles = "Admin,Supplier,Shopper")]
    public async Task<IActionResult> GetAll(KeywordViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<KeywordDto>(viewModel);
        var response = await service.GetAllAsync(dto, cancellationToken);
        var result = mapper.Map<List<ProvinceKeyValueViewModel>>(response.Data);
        return new AppHttpResponse<List<IdTitleDto>>(response).Create(result);
    }
}