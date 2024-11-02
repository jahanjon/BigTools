using Common.Enums;

namespace Domain.Dto.Communication;

public class MessageSendDto
{
    public int UserId { get; set; }
    public string Content { get; set; }
    public PlatformType PlatformType { get; set; }
}