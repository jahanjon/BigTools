using Domain.Dto.Common;
using Domain.Dto.Place;
using ViewModel.Place;

namespace API.Mappers.Place;

public class ProvinceMapper : AutoMapper.Profile
{
    public ProvinceMapper()
    {
        CreateMap<IdTitleDto, ProvinceKeyValueViewModel>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Title));
        CreateMap<ProvinceIdViewModel, ProvinceIdDto>();
    }
}