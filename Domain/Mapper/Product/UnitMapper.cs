using Domain.Dto.Product;
using Domain.Entity.Product;

namespace Domain.Mapper.Product;

public class UnitProfile : AutoMapper.Profile
{
    public UnitProfile()
    {
        CreateMap<UnitDto, Unit>();
        CreateMap<UnitUpdateDto, Unit>();
        CreateMap<UnitCreateDto, Unit>();
    }
}