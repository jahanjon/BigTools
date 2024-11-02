using Kavenegar.Core.Models.Enums;

namespace Domain.Dto.Communication;

public class MessageFilterDto
{
    public MessageStatus? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}