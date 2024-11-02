using Domain.Dto.LandingSearch;
using ViewModel.LandingSearch;

namespace API.Mappers.LandingSearch;

public class LandingSearchProFile : AutoMapper.Profile
{
    public LandingSearchProFile()
    {
        CreateMap<GoodSearchViewModel, GoodSearchDto>().ReverseMap();
        CreateMap<GoodSearchResultDto, GoodSearchResultViewModel>().ReverseMap();
        CreateMap<GoodDiscountSearchResultDto, GoodDiscountSearchResultViewModel>().ReverseMap();
        CreateMap<SupplierSearchViewModel, SupplierSearchDto>().ReverseMap();
        CreateMap<SupplierSearchResultDto, SupplierSearchResultViewModel>().ReverseMap();
    }
}