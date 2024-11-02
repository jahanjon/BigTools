using AutoMapper;
using Domain.Dto.LandingSearch;
using Domain.Service.LandingSearch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.LandingSearch;

namespace API.Areas.LandingSearch.Controllers;

public class LandingSearchController : BaseLandingSearchController
{
    private readonly ILandingSearchService _landingSearchService;
    private readonly IMapper _mapper;

    public LandingSearchController(
        ILandingSearchService landingSearchService,
        IMapper mapper)
    {
        _landingSearchService = landingSearchService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Roles = "Supplier,Shopper")]
    public async Task<IActionResult> SearchGoods(GoodSearchViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<GoodSearchDto>(viewModel);
        var response = await _landingSearchService.SearchGoods(dto, cancellationToken);
        var result = _mapper.Map<List<GoodSearchResultViewModel>>(response.Data);
        return new AppHttpResponse<List<GoodSearchResultDto>>(response).Create(result);
    }


    [HttpPost]
    [Authorize(Roles = "Supplier,Shopper")]
    public async Task<IActionResult> SearchSuppliers(SupplierSearchViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<SupplierSearchDto>(viewModel);
        var response = await _landingSearchService.SearchSuppliers(dto, cancellationToken);
        var result = _mapper.Map<List<SupplierSearchResultViewModel>>(response.Data);
        return new AppHttpResponse<List<SupplierSearchResultDto>>(response).Create(result);
    }
}