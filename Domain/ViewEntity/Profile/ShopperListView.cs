using Domain.ViewEntity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ViewEntity.Profile;

public class ShopperListView : IViewEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NationCode { get; set; }
    public string Mobile { get; set; }
    public bool IsActive { get; set; }
}

public class ShopperListViewConfiguration : IEntityTypeConfiguration<ShopperListView>
{
    public void Configure(EntityTypeBuilder<ShopperListView> builder)
    {
        builder.HasKey(x => x.Id);
    }
}