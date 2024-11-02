using Domain.Entity.Financial;
using Microsoft.Extensions.Localization;

namespace Domain.Extensions;

public static class GoodDiscountExtensions
{
    public static string GetDiscountResult(this GoodDiscount goodDiscount, IStringLocalizerFactory localizerFactory)
    {
        var localizer = localizerFactory.Create(typeof(GoodDiscountExtensions));
        var result =
            goodDiscount.InvoiceDiscountPercent > 0 ?
                !string.IsNullOrEmpty(goodDiscount.GiftItem) ?
                    $" {goodDiscount.InvoiceDiscountPercent} {localizer["InvoiceDiscountPercent"]} " + $"+ {localizer["GiftItem"]} {goodDiscount.GiftItem} " :
                    $"{goodDiscount.InvoiceDiscountPercent} {localizer["InvoiceDiscountPercent"]}" :
                goodDiscount.GoodDiscountPercent > 0 ?
                    !string.IsNullOrEmpty(goodDiscount.GiftItem) ?
                        $" {goodDiscount.GoodDiscountPercent} {localizer["GoodDiscountPercent"]} " + $"+ {localizer["GiftItem"]} {goodDiscount.GiftItem} " : $" {goodDiscount.GoodDiscountPercent} {localizer["GoodDiscountPercent"]} " :
                    $"{localizer["GiftItem"]} {goodDiscount.GiftItem}";

        return result;
    }
}

//public class ExtentionGood
//{
//    private readonly IStringLocalizer<ExtentionGood> localizer;

//    public ExtentionGood(IStringLocalizer<ExtentionGood> localizer)
//    {
//        this.localizer = localizer;
//    }
//}