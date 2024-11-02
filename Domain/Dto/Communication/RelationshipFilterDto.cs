using Common.Enums;

namespace Domain.Dto.Communication;

public class RelationshipFilterDto
{
    public int CurrentMemberId { get; set; }
    public RelationshipMemberType CurrentMemberType { get; set; }
    //public RelationshipType? RelationshipType { get; set; }
    //public RelationshipState? Status { get; set; }
    //public string? AcceptorName { get; set; }
    //public string? AcceptorNationalId { get; set; }
    //public string? AcceptorPhoneNumber { get; set; }
}