using Common.Enums;
using Domain.Entity.Base;
using Domain.Entity.Identity;
using Domain.Enums;
using Kavenegar.Core.Models.Enums;

namespace Domain.Entity.Communication;

public class Message : BaseEntity
{
    public string Content { get; set; }
    public MessageContentType ContentType { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public MessageStatus Status { get; set; }
    public PlatformType PlatformType { get; set; }
    public long MessageBackId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}