using Domain.Dto.Common;
using Domain.Dto.Product;
using Domain.Entity.Product;

namespace Domain.Mapper.Product;

public class BrandProFile : AutoMapper.Profile
{
    public BrandProFile()
    {
        CreateMap<BrandCreateDto, Brand>();
        CreateMap<Brand, BrandWithDetailDto>();
        CreateMap<Brand, IdTitleDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name));
    }
}