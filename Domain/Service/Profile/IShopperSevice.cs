using Domain.Dto.Common;
using Domain.Dto.File;
using Domain.Dto.Shopper;

namespace Domain.Service.Profile;

public interface IShopperSevice
{
    Task<ServiceResult> CreateAsync(ShopperCreateDto dto, int userId, CancellationToken cancellationToken);
    Task<ServiceResult<PagedListDto<ShopperListDto>>> GetListAsync(RequestedPageDto<ShopperListFilterDto> dto, CancellationToken cancellationToken);
    Task<ServiceResult> ToggleActivateAsync(int id, CancellationToken cancellationToken);
    Task<ServiceResult<ShopperFullDto, string>> GetAsync(int id, int userId, CancellationToken cancellationToken);
    Task<ServiceResult> UpdateAsync(ShopperUpdateDto dto, int userId, CancellationToken cancellationToken);
    Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(string keyword, int userId, CancellationToken cancellationToken);
    Task<ServiceResult> AddFileAsync(ShopperFileDto dto, int userId, CancellationToken cancellationToken);
    Task<ServiceResult> RemoveFileAsync(ShopperFileDto dto, int userId, CancellationToken cancellationToken);
    Task<ServiceResult> UpdateMainFileAsync(ShopperFileUpdateDto dto, int userId, CancellationToken cancellationToken);
    Task<ServiceResult<List<FileDto>>> GetFilesAsync(int id, CancellationToken cancellationToken);
}