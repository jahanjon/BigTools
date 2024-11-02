using Domain.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Product;

public class PackageType : BaseEntity
{
    public string Title { get; set; }
}

public class PackageTypeConfiguration : IEntityTypeConfiguration<PackageType>
{
    public void Configure(EntityTypeBuilder<PackageType> builder)
    {
        builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
    }
}