using AutoMapper;
using Domain.Dto.Common;
using Domain.Dto.Communication;
using Domain.Entity.Communication;
using Domain.Service.Communication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Common;
using ViewModel.Communication;

namespace API.Areas.Communication.Controllers;

public class MessageController(IMessageService messageService, IMapper mapper) : BaseCommunicationController
{

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetListAsync(RequestedPageViewModel<MessageFilterViewModel> viewModel, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<RequestedPageDto<MessageFilterDto>>(viewModel);
        var response = await messageService.GetListAsync(dto, cancellationToken);
        var result = mapper.Map<PagedListViewModel<MessageViewModel>>(response.Data);
        return new AppHttpResponse<PagedListDto<Message>>(response).Create(result);
    }
}