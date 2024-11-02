using Common.Enums;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Communication;

public class RelationshipFilterViewModel
{
    public int CurrentMemberId => CurrentMemberType == RelationshipMemberType.Supplier ? new SupplierEncryptedJsonConverter().Read(CurrentMemberIdString).Value : CurrentMemberType == RelationshipMemberType.Shopper ? new ShopperEncryptedJsonConverter().Read(CurrentMemberIdString).Value : 0;
    public string CurrentMemberIdString { get; set; }
    public RelationshipMemberType CurrentMemberType { get; set; }
    //public RelationshipType? RelationshipType { get; set; }
    //public RelationshipState? Status { get; set; }
    //public string? AcceptorName { get; set; }
    //public string? AcceptorNationalId { get; set; }
    //public string? AcceptorPhoneNumber { get; set; }
}