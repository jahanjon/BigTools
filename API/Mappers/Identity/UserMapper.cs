using Domain.Dto.Identity;
using Domain.Entity.Identity;
using ViewModel.Identity;

namespace API.Mappers.Identity;

public class UserProfile : AutoMapper.Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateViewModel, UserCreateDto>().ReverseMap();
        CreateMap<RegisterOrLoginViewModel, RegisterOrLoginDto>().ReverseMap();
        CreateMap<TokenViewModel, TokenDto>().ReverseMap();
        CreateMap<LoginViewModel, LoginDto>().ReverseMap();
        CreateMap<UserFilterDto, UserFilterViewModel>().ReverseMap();
        CreateMap<UserListDto, UserListViewModel>().ReverseMap();
        CreateMap<AccessToken, AccessTokenViewModel>();
        CreateMap<UserPositionDto, UserPositionViewModel>().ForMember(x => x.Shoppers, y => y.MapFrom(vm => vm.Shoppers))
            .ForMember(x => x.Suppliers, y => y.MapFrom(vm => vm.Suppliers));
    }
}