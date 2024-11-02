using Common.Enums;
using Domain.Enums;
using Kavenegar.Core.Models.Enums;

namespace ViewModel.Communication;

public class MessageViewModel
{
    public int Id { get; set; }
    public string Content { get; set; }
    public MessageContentType ContentType { get; set; }
    public string UserName { get; set; }
    public int UserId { get; set; }
    public MessageStatus Status { get; set; }
    public PlatformType PlatformType { get; set; }
}