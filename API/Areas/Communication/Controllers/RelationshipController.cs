using AutoMapper;
using Domain.Dto.Common;
using Domain.Dto.Communication;
using Domain.Service.Communication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Common;
using ViewModel.Communication;

namespace API.Areas.Communication.Controllers;

public class RelationshipController : BaseCommunicationController
{
    private readonly IMapper _mapper;
    private readonly IRelationshipService _relationshipService;

    public RelationshipController(IRelationshipService relationshipService, IMapper mapper)
    {
        _relationshipService = relationshipService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Roles = "Supplier,Shopper")]
    public async Task<IActionResult> CreateAsync(RelationshipCreateViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<RelationshipCreateDto>(viewModel);
        var response = await _relationshipService.CreateAsync(dto, UserId, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize]
    [Authorize(Roles = "Supplier,Shopper")]
    public async Task<IActionResult> AnswerAsync(RelationshipAnswerViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<RelationshipAnswerDto>(viewModel);
        var response = await _relationshipService.AnswerAsync(dto, UserId, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Supplier,Shopper")]
    public async Task<IActionResult> GetListAsync([FromBody] RequestedPageViewModel<RelationshipFilterViewModel> viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<RequestedPageDto<RelationshipFilterDto>>(viewModel);
        var response = await _relationshipService.GetListAsync(dto, cancellationToken);
        var result = _mapper.Map<PagedListViewModel<RelationshipListViewModel>>(response.Data);
        return new AppHttpResponse<PagedListDto<RelationshipListDto>>(response).Create(result);
    }
}