using AutoMapper;
using Common.Enums;
using Domain;
using Domain.Dto.Common;
using Domain.Dto.Product;
using Domain.Entity.Product;
using Domain.Repository.Product;
using Domain.Service;
using Domain.Service.Product;
using Microsoft.Extensions.Localization;
using Service.Identity;

namespace Service.Product;

public class BrandService : IBrandService
{
    private readonly IFileService _fileService;
    private readonly IStringLocalizer<BrandService> _localizer;
    private readonly IMapper _mapper;
    private readonly IBrandRepository _repository;

    public BrandService(IMapper mapper,
        IBrandRepository repository,
        IFileService fileService,
        IStringLocalizer<BrandService> localizer)
    {
        _mapper = mapper;
        _repository = repository;
        _localizer = localizer;
        _fileService = fileService;
    }

    public async Task<ServiceResult> CreateAsync(BrandCreateDto dto, CancellationToken cancellationToken)
    {
        var brand = _mapper.Map<Brand>(dto);

        brand.Enabled = true;

        await _repository.AddAsync(brand, cancellationToken);

        return new ServiceResult(_localizer["BrandCreated"]);
    }

    public async Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync(cancellationToken);

        return new ServiceResult<Dictionary<int, string>>(result);
    }

    public async Task<ServiceResult<PagedListDto<BrandDto>>> GetListAsync(RequestedPageDto<BrandFilterDto> dto, CancellationToken cancellationToken)
    {
        var result = await _repository.GetListAsync(dto, cancellationToken);

        foreach (var brand in result.Data)
        {
            brand.LogoLink = (await GetDownloadLinkAsync(brand.Logo, FileType.BrandLogo, cancellationToken)).Data;
        }

        return new ServiceResult<PagedListDto<BrandDto>>(result);
    }

    public Task<ServiceResult<string>> GetDownloadLinkAsync(Guid id, FileType type, CancellationToken cancellationToken)
    {
        return _fileService.GetLinkAsync(id, type, cancellationToken);
    }

    public async Task<ServiceResult<BrandWithDetailDto>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var brand = await _repository.GetByIdAsync(cancellationToken, id);

        await _repository.LoadReferenceAsync(brand, x => x.Owner, cancellationToken);

        var result = _mapper.Map<BrandWithDetailDto>(brand);

        result.LogoFileLink = (await GetDownloadLinkAsync(brand.LogoFileGuid, FileType.BrandLogo, cancellationToken)).Data;

        if (brand.PriceListGuid != null && brand.PriceListGuid != Guid.Empty)
        {
            result.PriceListLink = (await GetDownloadLinkAsync(brand.PriceListGuid.GetValueOrDefault(), FileType.BrandPriceList, cancellationToken)).Data;
        }

        return new ServiceResult<BrandWithDetailDto>(result);
    }

    public async Task<ServiceResult> UpdateAsync(BrandUpdateDto dto, CancellationToken cancellationToken)
    {
        var brand = await _repository.GetByIdAsync(cancellationToken, dto.Id);

        brand.Name = dto.Name;
        brand.EnName = dto.EnName;
        brand.OwnerId = dto.OwnerId;
        brand.LogoFileGuid = dto.LogoFileGuid;
        brand.PriceListGuid = dto.PriceListGuid;

        await _repository.UpdateAsync(brand, cancellationToken);

        return new ServiceResult(_localizer["BrandUpdated"]);
    }

    public async Task<ServiceResult> ToggleAsync(int id, CancellationToken cancellationToken)
    {
        var brand = await _repository.GetByIdAsync(cancellationToken, id);
        brand.Enabled = !brand.Enabled;
        await _repository.UpdateAsync(brand, cancellationToken);
        return new ServiceResult(_localizer["BrandToggled"]);
    }

    public async Task<ServiceResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var brand = await _repository.GetByIdAsync(cancellationToken, id);
        await _repository.DeleteAsync(brand, cancellationToken);
        return new ServiceResult(_localizer["BrandDeleted"]);
    }
}