using Domain.Dto.Common;
using Domain.Entity.Category;

namespace Domain.Mapper.Category;

public class CategoryLevel1Profile : AutoMapper.Profile
{
    public CategoryLevel1Profile()
    {
        CreateMap<CategoryLevel1, IdTitleDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name));
    }
}