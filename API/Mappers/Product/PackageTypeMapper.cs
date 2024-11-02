using Domain.Dto.Product;
using ViewModel.Product;

namespace API.Mappers.Product;

public class PackageTypeProfile : AutoMapper.Profile
{
    public PackageTypeProfile()
    {
        CreateMap<PackageTypeViewModel, PackageTypeDto>().ReverseMap();
        CreateMap<PackageUpdateViewModel, PackageTypeUpdateDto>();
        CreateMap<PackageTypeDeleteViewModel, PackageTypeUpdateDto>().ReverseMap();
        CreateMap<PackageTypeCreateViewModel, PackageTypeCreateDto>();
        CreateMap<KeyValuePair<int, string>, PackageTypeKeyValueViewModel>();
    }
}