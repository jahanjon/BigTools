using Domain.Dto.Common;
using Domain.Dto.Supplier;
using ViewModel.Profile;

namespace API.Mappers.Profile;

public class SupplierProfile : AutoMapper.Profile
{
    public SupplierProfile()
    {
        CreateMap<SupplierCreateViewModel, SupplierCreateDto>().ReverseMap();
        CreateMap<SupplierListViewModel, SupplierListDto>().ReverseMap();
        CreateMap<SupplierListFilterDto, SupplierListFilterViewModel>().ReverseMap();
        CreateMap<SupplierActivateDto, SupplierActivateViewModel>().ReverseMap();
        CreateMap<SupplyDetailsDto, SupplyDetailsViewModel>().ReverseMap();
        CreateMap<SupplierWithDetailDto, SupplierWithDetailViewModel>().ReverseMap();
        CreateMap<SupplierUpdateDto, SupplierUpdateViewModel>().ReverseMap();
        CreateMap<SupplierUpdateFileViewModel, SupplierUpdateFileDto>().ReverseMap();
        CreateMap<SupplierFileViewModel, SupplierFileDto>().ReverseMap();
        CreateMap<KeyValuePair<int, string>, SupplierKeyValueViewModel>()
            .ForMember(
                x => x.Key,
                opt => opt.MapFrom(src => src.Key)
            )
            .ForMember(
                x => x.Value,
                opt => opt.MapFrom(src => src.Value)
            );
        CreateMap<IdTitleDto, SupplierKeyValueViewModel>().ForMember(x => x.Key, y => y.MapFrom(vm => vm.Id))
            .ForMember(x => x.Value, y => y.MapFrom(vm => vm.Title));
    }
}