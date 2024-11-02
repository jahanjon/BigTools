using Domain.Dto.Communication;
using ViewModel.Communication;

namespace API.Mappers.Communication;

public class RelationshipProFile : AutoMapper.Profile
{

    public RelationshipProFile()
    {
        CreateMap<RelationshipCreateDto, RelationshipCreateViewModel>().ReverseMap();
        CreateMap<RelationshipAnswerViewModel, RelationshipAnswerDto>().ReverseMap();
        CreateMap<RelationshipListDto, RelationshipListViewModel>().ReverseMap();
        CreateMap<RelationshipFilterDto, RelationshipFilterViewModel>().ReverseMap();
    }
}