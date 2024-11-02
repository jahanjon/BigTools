using Domain.Dto.File;
using ViewModel.File;

namespace API.Mappers.File;

public class FileProfile : AutoMapper.Profile
{
    public FileProfile()
    {
        CreateMap<FileDto, FileViewModel>().ReverseMap();

    }
}