using Domain.Dto.Financial;
using Domain.Entity.Financial;
using ViewModel.Financial;

namespace API.Mappers.Financial;

public class GoodDiscountProFile : AutoMapper.Profile
{
    //private readonly IStringLocalizer<GoodDiscountProFile> _localizer;

    public GoodDiscountProFile( /*IStringLocalizer<GoodDiscountProFile> localizer*/)
    {
        CreateMap<GoodDiscountCreateViewModel, GoodDiscountCreateDto>();
        CreateMap<GoodDiscountFilterViewModel, GoodDiscountFilterDto>().ReverseMap();
        CreateMap<GoodDiscountDto, GoodDiscountViewModel>().ReverseMap();
        CreateMap<GoodDiscountSummaryDto, GoodDiscountSummaryViewModel>().ReverseMap();
        CreateMap<GoodDiscountKeyValueDto, GoodDiscountKeyValueViewModel>().ReverseMap();
        CreateMap<GoodDiscount, GoodDiscountSummaryDto>();
        //    .ForMember(
        //    x => x.DiscountResult,
        //    opt => opt.MapFrom( src => src.InvoiceDiscountPercent > 0 ? !string.IsNullOrEmpty(src.GiftItem) ? $" {src.InvoiceDiscountPercent} {_localizer["InvoiceDiscountPercent"]} " + $"+ {_localizer["GiftItem"]} {src.GiftItem} " : $"{src.InvoiceDiscountPercent} {_localizer["InvoiceDiscountPercent"]}" :
        //        src.GoodDiscountPercent > 0 ? !string.IsNullOrEmpty(src.GiftItem) ? $" {src.GoodDiscountPercent} {_localizer["GoodDiscountPercent"]} " + $"+ {_localizer["GiftItem"]} {src.GiftItem} " : $" {src.GoodDiscountPercent} {_localizer["GoodDiscountPercent"]} " :
        //        $"{_localizer["GiftItem"]} {src.GiftItem}")
        //    );
        //_localizer = localizer;
    }
}