using AutoMapper;
using Domain;
using Domain.Dto.Common;
using Domain.Dto.Product;
using Domain.Entity.Product;
using Domain.Repository.Product;
using Domain.Service.Product;
using Microsoft.Extensions.Localization;
using Service.Identity;

namespace Service.Product;

public class PackageTypeService : IPackageTypeService
{
    private readonly IStringLocalizer<PackageTypeService> _localizer;
    private readonly IMapper _mapper;
    private readonly IPackageTypeRepository _repository;

    public PackageTypeService(IPackageTypeRepository repository,
        IMapper mapper,
        IStringLocalizer<PackageTypeService> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<ServiceResult<bool, string>> CreateAsync(PackageTypeCreateDto dto,
        CancellationToken cancellationToken)
    {
        var newPackageType = _mapper.Map<PackageType>(dto);

        var validationResult = await ValidateAsync(newPackageType, cancellationToken);

        if (!validationResult.IsSuccess)
        {
            return validationResult;
        }

        newPackageType.CreatedAt = DateTime.UtcNow;

        await _repository.AddAsync(newPackageType, cancellationToken);

        return new ServiceResult<bool, string>(true);
    }

    public async Task<ServiceResult<PagedListDto<PackageTypeDto>>> GetListAsync(RequestedPageDto<KeywordDto> dto,
        CancellationToken cancellationToken)
    {
        var result = await _repository.GetListAsync(dto, cancellationToken);

        return new ServiceResult<PagedListDto<PackageTypeDto>>(result);
    }

    public async Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        return new ServiceResult<Dictionary<int, string>>(result);
    }

    public async Task<ServiceResult<bool, string>> UpdateAsync(PackageTypeUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var newPackageType = _mapper.Map<PackageType>(dto);

        var validationResult = await ValidateAsync(newPackageType, cancellationToken);

        if (!validationResult.IsSuccess)
        {
            return validationResult;
        }

        var packageType = await _repository.GetByIdAsync(cancellationToken, newPackageType.Id);

        packageType.Title = newPackageType.Title;
        packageType.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(packageType, cancellationToken);

        return new ServiceResult<bool, string>(true);
    }

    public async Task<ServiceResult<bool, string>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var packageType = await _repository.GetByIdAsync(cancellationToken, id);

        await _repository.DeleteAsync(packageType, cancellationToken);

        return new ServiceResult<bool, string>(true);
    }

    private async Task<ServiceResult<bool, string>> ValidateAsync(PackageType packageType,
        CancellationToken cancellationToken)
    {
        var isTitleUnique = await _repository.IsTitleUniqueAsync(packageType.Id, packageType.Title, cancellationToken);

        return !isTitleUnique ? new ServiceResult<bool, string>(_localizer["TitleAlreadyExists"]) : new ServiceResult<bool, string>(true);
    }
}