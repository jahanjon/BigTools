using Domain.Dto.Common;
using Domain.Dto.Identity;
using Domain.Entity.Identity;

namespace Domain.Service.Identity;

public interface IUserService
{
    Task<ServiceResult<User>> CreateAsync(UserCreateDto userDto, CancellationToken cancellationToken);
    Task<ServiceResult<bool, string>> RegisterOrLoginAsync(RegisterOrLoginDto dto, CancellationToken cancellationToken);
    Task<ServiceResult<AccessToken, List<string>>> GetTokenAsync(TokenDto dto, CancellationToken cancellationToken);
    Task<ServiceResult<AccessToken, string>> LoginAsync(LoginDto dto, CancellationToken cancellationToken);
    Task<ServiceResult<PagedListDto<UserListDto>>> GetListAsync(RequestedPageDto<UserFilterDto> dto, CancellationToken cancellationToken);
    Task<ServiceResult<UserPositionDto>> GetAllPositionAsync(int userId, CancellationToken cancellationToken);
}