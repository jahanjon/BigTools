using Domain.Dto.Product;
using ViewModel.Product;

namespace API.Mappers.Product;

public class UnitProfile : AutoMapper.Profile
{
    public UnitProfile()
    {
        CreateMap<UnitViewModel, UnitDto>().ReverseMap();
        CreateMap<UnitUpdateViewModel, UnitUpdateDto>();
        CreateMap<UnitDeleteViewModel, UnitUpdateDto>().ReverseMap();
        CreateMap<UnitCreateViewModel, UnitCreateDto>();
        CreateMap<KeyValuePair<int, string>, UnitKeyValueViewModel>();
    }
}