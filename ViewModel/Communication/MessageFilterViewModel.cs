using Kavenegar.Core.Models.Enums;

namespace ViewModel.Communication;

public class MessageFilterViewModel
{
    public MessageStatus? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}