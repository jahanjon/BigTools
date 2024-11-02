using Domain.Dto.Common;
using Domain.Entity.Place;

namespace Domain.Mapper.Place;

public class CityProfile : AutoMapper.Profile
{
    public CityProfile()
    {
        CreateMap<City, IdTitleDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name));
    }
}