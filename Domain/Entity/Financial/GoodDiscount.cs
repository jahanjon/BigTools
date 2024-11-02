using Common.Enums;
using Domain.Entity.Base;
using Domain.Entity.Product;
using Domain.Entity.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Financial;

public class GoodDiscount : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    public string ConditionDescription { get; set; }
    public ICollection<GoodCode> Goods { get; set; }
    public SaleType SaleType { get; set; }
    public PaymentType PaymentType { get; set; }
    public int? PaymentDurationDays { get; set; }
    public double? AmountMaxLimit { get; set; }
    public double? AmountMinLimit { get; set; }
    public double? CostMinLimit { get; set; }
    public int? ShopperRankLimit { get; set; }
    public int? InvoiceDiscountPercent { get; set; }
    public int? GoodDiscountPercent { get; set; }
    public string? GiftItem { get; set; }


    //public static string GetDiscountResult(IStringLocalizerFactory factory)
    //{

    //}
}

public class GoodDiscountConfiguration : IEntityTypeConfiguration<GoodDiscount>
{
    public void Configure(EntityTypeBuilder<GoodDiscount> builder)
    {
        builder.HasMany(gd => gd.Goods).WithMany();
    }
}