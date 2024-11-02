using Domain.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Category;

public class CategoryLevel3 : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public int ParentCategoryId { get; set; }
    public CategoryLevel2 ParentCategory { get; set; }
}

public class CategoryLevel3Configuration : IEntityTypeConfiguration<CategoryLevel3>
{
    public void Configure(EntityTypeBuilder<CategoryLevel3> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
    }
}