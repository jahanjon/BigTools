using Domain.Dto.Common;
using Domain.Dto.Product;
using Domain.Entity.Product;
using ViewModel.Product;

namespace API.Mappers.Product;

public class BrandProfile : AutoMapper.Profile
{
    public BrandProfile()
    {
        CreateMap<BrandCreateViewModel, BrandCreateDto>();
        CreateMap<BrandFilterViewModel, BrandFilterDto>();
        CreateMap<BrandViewModel, BrandDto>().ReverseMap();
        CreateMap<BrandWithDetailDto, BrandWithDetailViewModel>();
        CreateMap<BrandUpdateViewModel, BrandUpdateDto>();
        CreateMap<KeyValuePair<int, string>, BrandKeyValueViewModel>();
        CreateMap<IdTitleDto, BrandKeyValueViewModel>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Title));
        CreateMap<Brand, BrandSummaryDto>();
        CreateMap<BrandSummaryDto, BrandSummaryViewModel>().ReverseMap();
    }
}