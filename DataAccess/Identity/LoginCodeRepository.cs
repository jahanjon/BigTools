using DataAccess.Base;
using Domain.Entity.Identity;
using Domain.Repository.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Identity;

public class LoginCodeRepository(AppDbContext dbContext) : Repository<LoginCode>(dbContext), ILoginCodeRepository
{
    public Task<bool> HasAnyValidCodeByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return TableNoTracking.AnyAsync(x => x.UserId == userId && x.CreatedAt.AddMinutes(2) >= DateTime.UtcNow, cancellationToken);
    }

    public Task<LoginCode> GetValidCodeByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return Table.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }
}