using Domain.Dto.Category;

namespace Domain.Service.Category;

public interface ICategoryService
{
    public Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(CategoryGetAllDto dto, CancellationToken cancellationToken);
    Task<ServiceResult<List<CategoryLevel1Dto>>> GetAsync(CancellationToken cancellationToken);
    Task<ServiceResult> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken);
    Task<ServiceResult> UpdateAsync(CategoryUpdateDto dto, CancellationToken cancellationToken);
}