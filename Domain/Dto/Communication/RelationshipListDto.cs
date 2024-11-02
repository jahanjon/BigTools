using Common.Enums;

namespace Domain.Dto.Communication;

public class RelationshipListDto
{
    public int Id { get; set; }
    public int RequesterId { get; set; }
    public string RequesterName { get; set; }
    public RelationshipMemberType RequesterType { get; set; }
    public int RequesterCityId { get; set; }
    public string RequesterPhoneNumber { get; set; }
    public string? RequesterNationalId { get; set; }
    public int? AcceptorId { get; set; }
    public string AcceptorName { get; set; }
    public RelationshipMemberType AcceptorType { get; set; }
    public int AcceptorCityId { get; set; }
    public string AcceptorPhoneNumber { get; set; }
    public string? AcceptorNationalId { get; set; }
    public RelationshipType RelationshipType { get; set; }
    public RelationshipState Status { get; set; }
    public string Description { get; set; }
    public bool IsNew { get; set; }
}