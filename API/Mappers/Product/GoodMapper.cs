using Domain.Dto.Product;
using ViewModel.Product;

namespace API.Mappers.Product;

public class GoodProFile : AutoMapper.Profile
{
    public GoodProFile()
    {
        CreateMap<GoodCreateViewModel, GoodCreateDto>();
        CreateMap<GoodFilterViewModel, GoodFilterDto>();
        CreateMap<GoodDto, GoodViewModel>();
        CreateMap<KeyValuePair<int, string>, GoodKeyValueViewModel>();
        CreateMap<GoodWithDetailsDto, GoodWithDetailsViewModel>();
    }
}