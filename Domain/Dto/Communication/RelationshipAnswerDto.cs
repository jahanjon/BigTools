using Common.Enums;

namespace Domain.Dto.Communication;

public class RelationshipAnswerDto
{
    public int Id { get; set; }
    public int AcceptorId { get; set; }
    public RelationshipMemberType AcceptorType { get; set; }
    public bool IsAccepted { get; set; }

    //colleague details
    public bool? HasKnown { get; set; }
    public DateTime? HasKnownFromYear { get; set; }
    public bool? HasTrade { get; set; }
    public DateTime? HasTradeFromYear { get; set; }
    public bool? Suggests { get; set; }
}