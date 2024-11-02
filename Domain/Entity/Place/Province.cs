using Domain.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Place;

public class Province : BaseEntity
{
    public string Name { get; set; }
    public ICollection<City> Cities { get; set; }
}

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
    }
}