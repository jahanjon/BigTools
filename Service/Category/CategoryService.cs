using AutoMapper;
using Common.Enums;
using Domain;
using Domain.Dto.Category;
using Domain.Entity.Category;
using Domain.Entity.Product;
using Domain.Repository.Base;
using Domain.Service.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Identity;

namespace Service.Category;

public class CategoryService : ICategoryService
{
    private readonly IRepository<CategoryLevel1> _categoryLevel1Repository;
    private readonly IRepository<CategoryLevel2> _categoryLevel2Repository;
    private readonly IRepository<CategoryLevel3> _categoryLevel3Repository;
    private readonly IRepository<Good> _goodRepository;
    private readonly IStringLocalizer<CategoryService> _localizer;
    private readonly IMapper _mapper;

    public CategoryService(
        IRepository<CategoryLevel1> categoryLevel1Repository,
        IRepository<CategoryLevel2> categoryLevel2Repository,
        IRepository<CategoryLevel3> categoryLevel3Repository,
        IMapper mapper,
        IRepository<Good> goodRepository,
        IStringLocalizer<CategoryService> localizer)
    {
        _categoryLevel1Repository = categoryLevel1Repository;
        _categoryLevel2Repository = categoryLevel2Repository;
        _categoryLevel3Repository = categoryLevel3Repository;
        _mapper = mapper;
        _goodRepository = goodRepository;
        _localizer = localizer;
    }


    public async Task<ServiceResult> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken)
    {
        //Check Name
        if (string.IsNullOrEmpty(dto.Name))
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["NameCanNotBeEmpty"]);
        }

        if (await _categoryLevel1Repository.TableNoTracking.AnyAsync(e => e.Name == dto.Name) ||
            await _categoryLevel2Repository.TableNoTracking.AnyAsync(e => e.Name == dto.Name) ||
            await _categoryLevel3Repository.TableNoTracking.AnyAsync(e => e.Name == dto.Name))
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["NameIsDuplicate"]);
        }

        //Check Code
        if (string.IsNullOrEmpty(dto.Code))
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["CodeCanNotBeEmpty"]);
        }

        if (await _categoryLevel1Repository.TableNoTracking.AnyAsync(e => e.Code == dto.Code) ||
            await _categoryLevel2Repository.TableNoTracking.AnyAsync(e => e.Code == dto.Code) ||
            await _categoryLevel3Repository.TableNoTracking.AnyAsync(e => e.Code == dto.Code))
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["CodeIsDuplicate"]);
        }

        if (dto.Level == 1)
        {
            var category = new CategoryLevel1
            {
                Name = dto.Name,
                Code = dto.Code,
                Enabled = true
            };

            await _categoryLevel1Repository.AddAsync(category, cancellationToken);
        }
        else if (dto.Level == 2)
        {
            if (dto.ParentCategoryId == null || dto.ParentCategoryId == 0)
            {
                return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["EmptyParentId"]);
            }

            var parent = await _categoryLevel1Repository.Entities
                .Where(e => e.Id == dto.ParentCategoryId).SingleOrDefaultAsync();

            if (parent == null)
            {
                return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["ParentNotFound"]);
            }

            var category = new CategoryLevel2
            {
                Name = dto.Name,
                Code = dto.Code,
                ParentCategoryId = parent.Id,
                ParentCategory = parent,
                Enabled = true
            };
            await _categoryLevel2Repository.AddAsync(category, cancellationToken);
        }
        else if (dto.Level == 3)
        {
            if (dto.ParentCategoryId == null || dto.ParentCategoryId == 0)
            {
                return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["EmptyParentId"]);
            }

            var parent = await _categoryLevel2Repository.Entities
                .Where(e => e.Id == dto.ParentCategoryId).SingleOrDefaultAsync();

            if (parent == null)
            {
                return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["ParentNotFound"]);
            }

            var category = new CategoryLevel3
            {
                Name = dto.Name,
                Code = dto.Code,
                ParentCategoryId = parent.Id,
                ParentCategory = parent,
                Enabled = true
            };
            await _categoryLevel3Repository.AddAsync(category, cancellationToken);
        }
        else
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["IncorrectLevel"]);
        }

        return new ServiceResult(_localizer["CategoryCreated"]);
    }

    public async Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(CategoryGetAllDto dto, CancellationToken cancellationToken)
    {
        Dictionary<int, string> result;

        if (dto.Level == 1)
        {
            result = await _categoryLevel1Repository.TableNoTracking
                .ToDictionaryAsync(e => e.Id, e => e.Name);
        }
        else if (dto.Level == 2)
        {
            if (dto.ParentCategoryId == 0)
            {
                return new ServiceResult<Dictionary<int, string>>(
                    false, ApiResultStatusCode.BadRequest, null, _localizer["EmptyParentId"]);
            }

            result = await _categoryLevel2Repository.TableNoTracking
                .Where(e => e.ParentCategoryId == dto.ParentCategoryId)
                .ToDictionaryAsync(e => e.Id, e => e.Name);
        }
        else if (dto.Level == 3)
        {
            if (dto.ParentCategoryId == 0)
            {
                return new ServiceResult<Dictionary<int, string>>(
                    false, ApiResultStatusCode.BadRequest, null, _localizer["EmptyParentId"]);
            }

            result = await _categoryLevel3Repository.TableNoTracking
                .Where(e => e.ParentCategoryId == dto.ParentCategoryId)
                .ToDictionaryAsync(e => e.Id, e => e.Name);
        }
        else
        {
            return new ServiceResult<Dictionary<int, string>>(
                false, ApiResultStatusCode.BadRequest, null, _localizer["IncorrectLevel"]);
        }

        return new ServiceResult<Dictionary<int, string>>(result);
    }

    public async Task<ServiceResult<List<CategoryLevel1Dto>>> GetAsync(CancellationToken cancellationToken)
    {
        var categories = await _categoryLevel1Repository.TableNoTracking
            .Include(c => c.ChildCategories)
            .ThenInclude(cc => cc.ChildCategories)
            .ToListAsync();

        var mappedResult = _mapper.Map<List<CategoryLevel1Dto>>(categories);
        return new ServiceResult<List<CategoryLevel1Dto>>(mappedResult);
    }


    public async Task<ServiceResult> UpdateAsync(CategoryUpdateDto dto, CancellationToken cancellationToken)
    {
        if (dto.Level == 1)
        {
            var category = await _categoryLevel1Repository.Entities
                .Where(e => e.Id == dto.Id).SingleOrDefaultAsync();
            if (category is null)
            {
                return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["CategoryNotFound"]);
            }

            if (!string.IsNullOrEmpty(dto.Name))
            {
                category.Name = dto.Name;
            }

            if (!string.IsNullOrEmpty(dto.Code))
            {
                var leafChildIds = await _categoryLevel2Repository.TableNoTracking
                    .Where(e => e.ParentCategoryId == category.Id)
                    .Include(c => c.ChildCategories)
                    .SelectMany(c => c.ChildCategories.Select(l => l.Id))
                    .ToListAsync();

                var CodeUsed = await _goodRepository.TableNoTracking
                    .AnyAsync(g => leafChildIds.Contains(g.CategoryId));

                if (CodeUsed)
                {
                    return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["CodeHasDependency"]);
                }

                category.Code = dto.Code;
            }

            await _categoryLevel1Repository.UpdateAsync(category, cancellationToken);
        }
        else if (dto.Level == 2)
        {
            var category = await _categoryLevel2Repository.Entities
                .Where(e => e.Id == dto.Id).SingleOrDefaultAsync();

            if (category is null)
            {
                return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["CategoryNotFound"]);
            }

            if (!string.IsNullOrEmpty(dto.Name))
            {
                category.Name = dto.Name;
            }

            if (!string.IsNullOrEmpty(dto.Code))
            {
                var leafChildIds = await _categoryLevel3Repository.TableNoTracking
                    .Where(e => e.ParentCategoryId == dto.Id)
                    .Select(e => e.Id)
                    .ToListAsync();

                var CodeUsed = _goodRepository.TableNoTracking
                    .Any(g => leafChildIds.Contains(g.CategoryId));

                if (CodeUsed)
                {
                    return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["CodeHasDependency"]);
                }

                category.Code = dto.Code;
            }

            await _categoryLevel2Repository.UpdateAsync(category, cancellationToken);
        }
        else if (dto.Level == 3)
        {
            var category = await _categoryLevel3Repository.Entities
                .Where(e => e.Id == dto.Id).SingleOrDefaultAsync();
            if (category is null)
            {
                return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["CategoryNotFound"]);
            }

            if (!string.IsNullOrEmpty(dto.Name))
            {
                category.Name = dto.Name;
            }

            if (!string.IsNullOrEmpty(dto.Code))
            {
                var CodeUsed = await _goodRepository.TableNoTracking
                    .AnyAsync(g => g.CategoryId == category.Id);

                if (CodeUsed)
                {
                    return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["CodeHasDependency"]);
                }

                category.Code = dto.Code;
            }

            await _categoryLevel3Repository.UpdateAsync(category, cancellationToken);
        }
        else
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["IncorrectLevel"]);
        }


        return new ServiceResult(_localizer["CategoryUpdated"]);
    }
}