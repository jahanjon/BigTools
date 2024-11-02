using Common.Enums;
using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Communication;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Communication;

public class RelationshipAnswerViewModel
{
    [JsonConverter(typeof(RelationshipEncryptedJsonConverter))]
    public int Id { get; set; }
    public string AcceptorIdString { get; set; }
    public int AcceptorId => AcceptorType == RelationshipMemberType.Supplier ? new SupplierEncryptedJsonConverter().Read(AcceptorIdString).Value : AcceptorType == RelationshipMemberType.Shopper ? new ShopperEncryptedJsonConverter().Read(AcceptorIdString).Value : 0;
    public RelationshipMemberType AcceptorType { get; set; }
    public bool IsAccepted { get; set; }

    //colleague details
    public bool? HasKnown { get; set; }
    public DateTime? HasKnownFromYear { get; set; }
    public bool? HasTrade { get; set; }
    public DateTime? HasTradeFromYear { get; set; }
    public bool? Suggests { get; set; }
}