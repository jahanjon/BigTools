using AutoMapper;
using Domain.Dto.Category;
using Domain.Service.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Category;

namespace API.Areas.Category.Controllers;

public class CategoryController : BaseCategoryController
{
    private readonly IMapper _mapper;
    private readonly ICategoryService _service;

    public CategoryController(IMapper mapper, ICategoryService service)
    {
        _mapper = mapper;
        _service = service;
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync(CategoryCreateViewModel viewModel,
        CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<CategoryCreateDto>(viewModel);
        var response = await _service.CreateAsync(dto, cancellationToken);
        return new AppHttpResponse(response).Create();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(CategoryUpdateViewModel viewModel,
        CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<CategoryUpdateDto>(viewModel);
        var response = await _service.UpdateAsync(dto, cancellationToken);
        return new AppHttpResponse(response).Create();
    }


    [HttpPost]
    [Authorize(Roles = "Admin,Supplier,Shopper")]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var response = await _service.GetAsync(cancellationToken);
        var result = _mapper.Map<List<CategoryLevel1ViewModel>>(response.Data);
        return new AppHttpResponse<List<CategoryLevel1Dto>>(response).Create(result);
    }


    [HttpPost]
    [Authorize(Roles = "Supplier,Shopper")]
    public async Task<IActionResult> GetAllAsync(CategoryGetAllViewModel viewModel, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<CategoryGetAllDto>(viewModel);
        var response = await _service.GetAllAsync(dto, cancellationToken);
        var result = _mapper.Map<List<CategoryKeyValueViewModel>>(response.Data.ToList());
        return new AppHttpResponse<Dictionary<int, string>>(response).Create(result);
    }
}