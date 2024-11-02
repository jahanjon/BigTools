using Domain.Dto.Product;
using Domain.Entity.Product;

namespace Domain.Mapper.Product;

public class PackageTypeProfile : AutoMapper.Profile
{
    public PackageTypeProfile()
    {
        CreateMap<PackageTypeCreateDto, PackageType>();
        CreateMap<PackageTypeUpdateDto, PackageType>();
    }
}