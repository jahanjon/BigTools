using Domain.Entity.Base;
using Domain.Entity.Place;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Profile;

public class ShopperFriend : BaseEntity
{
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Description { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
    public int ShopperId { get; set; }
    public Shopper Shopper { get; set; }
}

public class ShopperFriendsConfiguration : IEntityTypeConfiguration<ShopperFriend>
{
    public void Configure(EntityTypeBuilder<ShopperFriend> builder)
    {
        builder.Property(p => p.Mobile).IsRequired().HasMaxLength(11);
        builder.HasOne(s => s.City);
    }
}