using Domain.ViewEntity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ViewEntity.Profile;

public class SupplierListView : IViewEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NationalId { get; set; }
    public string Mobile { get; set; }
    public bool IsActive { get; set; }
}

public class SupplierListViewConfiguration : IEntityTypeConfiguration<SupplierListView>
{
    public void Configure(EntityTypeBuilder<SupplierListView> builder)
    {
        builder.HasKey(x => x.Id);
    }
}