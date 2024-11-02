using Domain.Dto.Common;
using ViewModel.Common;

namespace API.Mappers.Common;

public class CommonProfile : AutoMapper.Profile
{
    public CommonProfile()
    {
        CreateMap<KeywordViewModel, KeywordDto>().ReverseMap();
        CreateMap(typeof(KeywordViewModel<>), typeof(KeywordDto<>)).ReverseMap();
        CreateMap<TitleCreateViewModel, TitleCreateDto>();
        CreateMap(typeof(RequestedPageViewModel<>), typeof(RequestedPageDto<>)).ReverseMap();
        CreateMap(typeof(KeyValuePair<,>), typeof(KeyValueViewModel<,>));
        CreateMap(typeof(PagedListDto<>), typeof(PagedListViewModel<>)).ReverseMap();
    }
}