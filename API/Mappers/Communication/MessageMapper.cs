using Domain.Dto.Communication;
using Domain.Entity.Communication;
using ViewModel.Communication;

namespace API.Mappers.Communication;

public class MessageProfile : AutoMapper.Profile
{
    public MessageProfile()
    {
        CreateMap<MessageFilterViewModel, MessageFilterDto>();
        CreateMap<Message, MessageViewModel>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
    }
}