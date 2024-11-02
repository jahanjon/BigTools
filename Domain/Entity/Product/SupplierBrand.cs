using Domain.Entity.Base;
using Domain.Entity.Profile;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Product;

public class SupplierBrand : IEntity
{
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    public SupplyType Type { get; set; }
}

public class SupplierBrandConfiguration : IEntityTypeConfiguration<SupplierBrand>
{
    public void Configure(EntityTypeBuilder<SupplierBrand> builder)
    {
        builder.HasKey(sb => new { sb.SupplierId, sb.BrandId, sb.Type });
    }
}