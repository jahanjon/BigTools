using Domain.Dto.Identity;

namespace Domain.Service.Identity;

public interface ILoginCodeService
{
    Task<ServiceResult<bool, string>> SendToMobileAsync(string mobile, CancellationToken cancellationToken);
    Task<ServiceResult<bool, string>> ValidateAsync(TokenDto dto, CancellationToken cancellationToken);
}