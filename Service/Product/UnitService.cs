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

public class UnitService : IUnitService
{
    private readonly IStringLocalizer<UnitService> _localizer;
    private readonly IMapper _mapper;
    private readonly IUnitRepository _repository;

    public UnitService(IUnitRepository repository,
        IMapper mapper,
        IStringLocalizer<UnitService> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<ServiceResult<bool, string>> CreateAsync(UnitCreateDto dto, CancellationToken cancellationToken)
    {
        var newUnit = _mapper.Map<Unit>(dto);

        newUnit.Enabled = true;

        var validationResult = await ValidateAsync(newUnit, cancellationToken);

        if (!validationResult.IsSuccess)
        {
            return validationResult;
        }

        await _repository.AddAsync(newUnit, cancellationToken);

        return new ServiceResult<bool, string>(true);
    }

    public async Task<ServiceResult<PagedListDto<UnitDto>>> GetListAsync(RequestedPageDto<KeywordDto> dto, CancellationToken cancellationToken)
    {
        var result = await _repository.GetListAsync(dto, cancellationToken);

        return new ServiceResult<PagedListDto<UnitDto>>(result);
    }

    public async Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        return new ServiceResult<Dictionary<int, string>>(result);
    }

    public async Task<ServiceResult<bool, string>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var unit = await _repository.GetByIdAsync(cancellationToken, id);
        await _repository.DeleteAsync(unit, cancellationToken);
        return new ServiceResult<bool, string>(true);
    }

    public async Task<ServiceResult<bool, string>> UpdateAsync(UnitUpdateDto dto, CancellationToken cancellationToken)
    {
        var newUnit = _mapper.Map<Unit>(dto);

        var validationResult = await ValidateAsync(newUnit, cancellationToken);
        if (!validationResult.IsSuccess)
        {
            return validationResult;
        }

        var oldUnit = await _repository.GetByIdAsync(cancellationToken, newUnit.Id);
        oldUnit.Title = newUnit.Title;

        await _repository.UpdateAsync(oldUnit, cancellationToken);
        return new ServiceResult<bool, string>(true);
    }

    private async Task<ServiceResult<bool, string>> ValidateAsync(Unit unit, CancellationToken cancellationToken)
    {
        var isTitleUnique = await _repository.IsTitleUniqueAsync(unit.Id, unit.Title, cancellationToken);

        return !isTitleUnique ? new ServiceResult<bool, string>(_localizer["TitleAlreadyExists"]) : new ServiceResult<bool, string>(true);
    }
}