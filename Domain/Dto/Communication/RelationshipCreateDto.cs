using Common.Enums;

namespace Domain.Dto.Communication;

public class RelationshipCreateDto
{
    public int RequesterId { get; set; }
    public RelationshipMemberType RequesterType { get; set; }
    public int? AcceptorId { get; set; }
    public RelationshipMemberType AcceptorType { get; set; }
    public bool AcceptorExists { get; set; }
    public string? AcceptorName { get; set; }
    public int? AcceptorCityId { get; set; }
    public string? AcceptorPhoneNumber { get; set; }
    public string? AcceptorNationalId { get; set; }
    public RelationshipType RelationshipType { get; set; }
    public string Description { get; set; }
}