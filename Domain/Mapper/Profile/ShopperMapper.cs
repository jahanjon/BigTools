using Domain.Dto.Shopper;
using Domain.Entity.Profile;

namespace Domain.Mapper.Profile;

public class ShopperProfile : AutoMapper.Profile
{
    public ShopperProfile()
    {
        CreateMap<ShopperCreateDto, Shopper>();
        CreateMap<ShopperFriendDto, ShopperFriend>().ReverseMap();
        CreateMap<Shopper, ShopperFullDto>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
            .ForMember(dest => dest.Friends, opt => opt.MapFrom(src => src.Friends))
            //.ForMember(dest => dest.HomeProvince, opt => opt.MapFrom(src => src.HomeCity.Province))
            .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.City.Province))
            .ForMember(dest => dest.LicenseImage, opt => opt.Ignore());
    }
}