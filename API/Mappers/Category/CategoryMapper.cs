using Domain.Dto.Category;
using Domain.Dto.Common;
using Domain.Entity.Category;
using ViewModel.Category;

namespace API.Mappers.Category;

public class CategoryProfile : AutoMapper.Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryCreateDto, CategoryCreateViewModel>().ReverseMap();
        CreateMap<CategoryLevel1, CategoryLevel1Dto>().ReverseMap();
        CreateMap<CategoryLevel2, CategoryLevel2Dto>().ReverseMap();
        CreateMap<CategoryLevel3, CategoryLevel3Dto>().ReverseMap();
        CreateMap<CategoryLevel1Dto, CategoryLevel1ViewModel>().ReverseMap();
        CreateMap<CategoryLevel2Dto, CategoryLevel2ViewModel>().ReverseMap();
        CreateMap<CategoryLevel3Dto, CategoryLevel3ViewModel>().ReverseMap();
        CreateMap<CategoryUpdateViewModel, CategoryUpdateDto>().ReverseMap();
        CreateMap<CategoryGetAllViewModel, CategoryGetAllDto>().ReverseMap();
        CreateMap<KeyValuePair<int, string>, CategoryKeyValueViewModel>().ReverseMap();
        CreateMap<IdTitleDto, CategoryLevel1KeyValueViewModel>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Title));
        CreateMap<CategoryKeyValueDto, CategoryKeyValueViewModel>().ReverseMap();
        CreateMap<CategoryLevel1, CategoryKeyValueDto>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));


    }
}