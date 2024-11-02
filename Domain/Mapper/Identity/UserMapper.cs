using Domain.Dto.Identity;
using Domain.Entity.Identity;

namespace Domain.Mapper.Identity;

public class UserCreateProfile : AutoMapper.Profile
{
    public UserCreateProfile()
    {
        CreateMap<UserCreateDto, User>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.UserName));
    }
}