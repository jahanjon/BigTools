using Domain.Dto.Common;
using ViewModel.Place;

namespace API.Mappers.Place;

public class CityProfile : AutoMapper.Profile
{
    public CityProfile()
    {
        CreateMap<IdTitleDto, CityKeyValueViewModel>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Title));
    }
}