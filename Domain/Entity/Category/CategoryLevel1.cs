using Domain.Entity.Base;
using Domain.Entity.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Category;

public class CategoryLevel1 : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public ICollection<Supplier> Suppliers { get; set; }
    public ICollection<Shopper> Shoppers { get; set; }
    public ICollection<CategoryLevel2> ChildCategories { get; set; }
}

public class CategoryLevel1Configuration : IEntityTypeConfiguration<CategoryLevel1>
{
    public void Configure(EntityTypeBuilder<CategoryLevel1> builder)
    {
        builder.HasMany(x => x.ChildCategories).WithOne(e => e.ParentCategory);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
    }
}