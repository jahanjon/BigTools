using Domain.Entity.Base;
using Domain.Entity.Category;
using Domain.Entity.Identity;
using Domain.Entity.Place;
using Domain.Entity.Shopping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Profile;

public class Shopper : BaseEntity
{
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Phone { get; set; }
    public string PersonName { get; set; }
    public string NationCode { get; set; }
    public DateTime? BirthDate { get; set; }
    public int HomeCityId { get; set; }
    //public City HomeCity { get; set; }
    public string HomePostalCode { get; set; }
    public string HomeAddress { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
    public string PostalCode { get; set; }
    public string Address { get; set; }
    public bool IsRent { get; set; }  //estate type : ejarei
    public int Area { get; set; }
    public bool HasLicense { get; set; }
    public string LicenseCode { get; set; }
    public bool IsRetail { get; set; } //Business type : khorde foroshi
    public Guid? LicenseImage { get; set; }
    //public Guid? BannerImage { get; set; }     //use FileRelation instead
    //public Guid? DocOrRentImage { get; set; }   //use FileRelation instead
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<CategoryLevel1> Categories { get; set; }
    public ICollection<ShopperFriend> Friends { get; set; }
    public ICollection<ShoppingCart> ShoppingCarts { get; set; }

    //public int? BrandId { get; set; }    // use ShopperBrand relation
    //public Brand? Brand { get; set; }
}

public class ShopperConfiguration : IEntityTypeConfiguration<Shopper>
{
    public void Configure(EntityTypeBuilder<Shopper> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.HasOne(s => s.City);
        //builder.HasOne(s => s.HomeCity);
        builder.HasOne(s => s.User);
        builder.HasMany(s => s.Categories).WithMany(x => x.Shoppers);
        builder.HasMany(s => s.Friends).WithOne(x => x.Shopper);
    }
}