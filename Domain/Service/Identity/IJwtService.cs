using Domain.Entity.Identity;

namespace Domain.Service.Identity;

public interface IJwtService
{
    Task<AccessToken> GenerateAsync(User user);
}