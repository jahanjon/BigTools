using Common.Enums;
using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Communication;
using ViewModel.EncryptedJsonConverters.Place;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Communication;

public class RelationshipListViewModel
{
    [JsonConverter(typeof(RelationshipEncryptedJsonConverter))]
    public int Id { get; set; }
    //todo: there is any annotation to hide this?
    public int RequesterId { protected get; set; }
    public string RequesterIdString => RequesterType == RelationshipMemberType.Supplier ? new SupplierEncryptedJsonConverter().Write(RequesterId) : RequesterType == RelationshipMemberType.Shopper ? new ShopperEncryptedJsonConverter().Write(RequesterId) : "";
    public string RequesterName { get; set; }
    public RelationshipMemberType RequesterType { get; set; }
    [JsonConverter(typeof(CityEncryptedJsonConverter))]
    public int RequesterCityId { get; set; }
    public string RequesterPhoneNumber { get; set; }
    public string? RequesterNationalId { get; set; }
    public int? AcceptorId { protected get; set; }
    public string? AcceptorIdString => AcceptorType == RelationshipMemberType.Supplier ? new SupplierEncryptedJsonConverter().Write(AcceptorId) : AcceptorType == RelationshipMemberType.Shopper ? new ShopperEncryptedJsonConverter().Write(AcceptorId) : "";
    public string AcceptorName { get; set; }
    public RelationshipMemberType AcceptorType { get; set; }
    [JsonConverter(typeof(CityEncryptedJsonConverter))]
    public int AcceptorCityId { get; set; }
    public string AcceptorPhoneNumber { get; set; }
    public string? AcceptorNationalId { get; set; }
    public RelationshipType RelationshipType { get; set; }
    public RelationshipState Status { get; set; }
    public string Description { get; set; }
    public bool IsNew { get; set; }
}