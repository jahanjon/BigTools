using Domain.Dto.Common;
using Domain.Dto.Shopper;
using ViewModel.Profile;

namespace API.Mappers.Profile;

public class ShopperMapper : AutoMapper.Profile
{
    public ShopperMapper()
    {
        CreateMap<ShopperCreateViewModel, ShopperCreateDto>();
        CreateMap<ShopperFriendViewModel, ShopperFriendDto>();
        CreateMap<ShopperListViewModel, ShopperListDto>().ReverseMap();
        CreateMap<ShopperListFilterDto, ShopperListFilterViewModel>().ReverseMap();
        CreateMap<IdTitleDto, ShopperKeyValueViewModel>()
            .ForMember(x => x.Key, y => y.MapFrom(vm => vm.Id))
            .ForMember(x => x.Value, y => y.MapFrom(vm => vm.Title));
        CreateMap<ShopperFriendDto, ShopperFriendViewModel>();
        CreateMap<ShopperFullDto, ShopperFullViewModel>();
        CreateMap<ShopperUpdateViewModel, ShopperUpdateDto>();
        CreateMap<ShopperFileViewModel, ShopperFileDto>().ReverseMap();
        CreateMap<ShopperFileUpdateViewModel, ShopperFileUpdateDto>().ReverseMap();
    }
}