using Domain.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Category;

public class CategoryLevel2 : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public int ParentCategoryId { get; set; }
    public CategoryLevel1 ParentCategory { get; set; }
    public ICollection<CategoryLevel3> ChildCategories { get; set; }
}

public class CategoryLevel2Configuration : IEntityTypeConfiguration<CategoryLevel2>
{
    public void Configure(EntityTypeBuilder<CategoryLevel2> builder)
    {
        builder.HasMany(x => x.ChildCategories).WithOne(e => e.ParentCategory);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
    }
}