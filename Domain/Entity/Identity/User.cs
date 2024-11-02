using Domain.Entity.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Identity;

public class User : IdentityUser<int>, IEntity
{
    public string Mobile { get; set; }
    public bool IsActive { get; set; }
    public bool IsNewUser { get; set; }
    public DateTimeOffset? LastLoginDate { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.Mobile).IsRequired().IsFixedLength().HasMaxLength(11);
    }
}