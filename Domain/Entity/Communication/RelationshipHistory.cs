using Common.Enums;
using Domain.Entity.Base;

namespace Domain.Entity.Communication;

public class RelationshipHistory : BaseEntity
{
    public int RelationshipId { get; set; }
    public Relationship Relationship { get; set; }
    public RelationshipMemberType PerformerType { get; set; }
    public int PerformerId { get; set; }
    public RelationshipState NewState { get; set; }
    public string Description { get; set; } = string.Empty;
}