using Domain.Dto.Common;
using Domain.Dto.Supplier;
using Domain.Entity.Profile;
using Domain.Repository.Base;

namespace Domain.Repository.Profile;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<PagedListDto<SupplierListDto>> GetListAsync(RequestedPageDto<SupplierListFilterDto> dto, CancellationToken cancellationToken);
    Task<int> GetUserIdBySupplierId(int id, CancellationToken cancellationToken);
    Task<Dictionary<int, string>> GetAllAsync(string keyword, int userId, bool isAdmin, CancellationToken cancellationToken);
    Task<Supplier> GetByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<bool> IsNationalIdUniqueAsync(string nationalId, CancellationToken cancellationToken);
    Task<bool> IsCompanyNationalIdUniqueAsync(string companyNationalId, CancellationToken cancellationToken);
    Task<bool> IsMobileUniqueAsync(string mobile, CancellationToken cancellationToken);
}