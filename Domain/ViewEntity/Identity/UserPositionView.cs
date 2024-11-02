using Common.Enums;
using Domain.ViewEntity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ViewEntity.Identity;

public class UserPositionView : IViewEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public UserPositionType PositionType { get; set; }
}

public class UserPositionViewConfiguration : IEntityTypeConfiguration<UserPositionView>
{
    public void Configure(EntityTypeBuilder<UserPositionView> builder)
    {
        builder.HasNoKey();
    }
}