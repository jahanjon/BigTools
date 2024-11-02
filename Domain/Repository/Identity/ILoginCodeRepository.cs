using Domain.Entity.Identity;
using Domain.Repository.Base;

namespace Domain.Repository.Identity;

public interface ILoginCodeRepository : IRepository<LoginCode>
{
    Task<bool> HasAnyValidCodeByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<LoginCode> GetValidCodeByUserIdAsync(int userId, CancellationToken cancellationToken);
}