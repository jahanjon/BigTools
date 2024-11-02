using Domain.Entity.Base;
using Domain.Entity.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Product;

public class ShopperBrand : IEntity
{
    public int ShopperId { get; set; }
    public Shopper Shopper { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
}

public class ShopperBrandConfiguration : IEntityTypeConfiguration<ShopperBrand>
{
    public void Configure(EntityTypeBuilder<ShopperBrand> builder)
    {
        builder.HasKey(shb => new { shb.ShopperId, shb.BrandId });
    }
}