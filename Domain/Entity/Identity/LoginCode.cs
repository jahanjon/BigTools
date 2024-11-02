using Common.Enums;
using Domain.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Identity;

public class LoginCode : BaseEntity
{
    public string Code { get; set; }
    public LoginCodeType Type { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}

public class LoginCodeConfiguration : IEntityTypeConfiguration<LoginCode>
{
    public void Configure(EntityTypeBuilder<LoginCode> builder)
    {
        builder.Property(l => l.Code).IsRequired().HasMaxLength(5);
        builder.HasOne(l => l.User);
    }
}