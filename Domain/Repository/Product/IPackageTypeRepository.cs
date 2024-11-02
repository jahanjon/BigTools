using Domain.Dto.Common;
using Domain.Dto.Product;
using Domain.Entity.Product;
using Domain.Repository.Base;

namespace Domain.Repository.Product;

public interface IPackageTypeRepository : IRepository<PackageType>
{
    Task<bool> IsTitleUniqueAsync(int id, string title, CancellationToken cancellationToken);

    Task<PagedListDto<PackageTypeDto>> GetListAsync(RequestedPageDto<KeywordDto> dto,
        CancellationToken cancellationToken);

    //Task<Dictionary<int, string>> GetPackageByIdAsync(int id, CancellationToken cancellationToken);

    Task<Dictionary<int, string>> GetAllAsync(CancellationToken cancellationToken);


    Task<bool> IsDataExistAsync(int id, CancellationToken cancellationToken);
}