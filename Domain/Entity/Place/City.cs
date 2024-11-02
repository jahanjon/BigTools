using Domain.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Place;

public class City : BaseEntity
{
    public string Name { get; set; }
    public int ProvinceId { get; set; }
    public Province Province { get; set; }
    public int Id { get; set; }
}

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.HasOne(c => c.Province).WithMany(p => p.Cities).HasForeignKey(c => c.ProvinceId);
    }
}