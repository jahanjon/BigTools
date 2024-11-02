using Common.Enums;
using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Place;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Communication;

public class RelationshipCreateViewModel
{
    //[JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    //public int RequesterId { get; set; }
    public string RequesterIdString { get; set; }
    public int RequesterId => RequesterType == RelationshipMemberType.Supplier ? new SupplierEncryptedJsonConverter().Read(RequesterIdString).Value : RequesterType == RelationshipMemberType.Shopper ? new ShopperEncryptedJsonConverter().Read(RequesterIdString).Value : 0;
    public RelationshipMemberType RequesterType { get; set; }
    public string? AcceptorIdString { get; set; }
    public int? AcceptorId => AcceptorType == RelationshipMemberType.Supplier ? new SupplierEncryptedJsonConverter().Read(AcceptorIdString) : AcceptorType == RelationshipMemberType.Shopper ? new ShopperEncryptedJsonConverter().Read(AcceptorIdString) : null;
    public RelationshipMemberType AcceptorType { get; set; }
    public bool AcceptorExists { get; set; }
    public string? AcceptorName { get; set; }
    [JsonConverter(typeof(CityEncryptedJsonConverter))]
    public int? AcceptorCityId { get; set; }
    public string? AcceptorPhoneNumber { get; set; }
    public string? AcceptorNationalId { get; set; }
    public RelationshipType RelationshipType { get; set; }
    public string Description { get; set; }
}