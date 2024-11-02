using Common.Enums;
using Domain.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Communication;

public class Relationship : BaseEntity
{
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
    public ICollection<RelationshipHistory> History { get; set; }
    public string Description { get; set; }

    //colleague details
    public bool? HasKnown { get; set; }
    public DateTime? HasKnownFromYear { get; set; }
    public bool? HasTrade { get; set; }
    public DateTime? HasTradeFromYear { get; set; }
    public bool? Suggests { get; set; }
}

public class RelationshipConfiguration : IEntityTypeConfiguration<Relationship>
{
    public void Configure(EntityTypeBuilder<Relationship> builder)
    {
        builder.HasMany(r => r.History).WithOne(h => h.Relationship);
    }
}