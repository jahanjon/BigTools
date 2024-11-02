using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Category;
using ViewModel.EncryptedJsonConverters.Place;
using ViewModel.EncryptedJsonConverters.Product;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.Profile;

public class SupplierUpdateViewModel
{
    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int Id { get; set; }
    public string? Name { get; set; }
    //public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Phone2 { get; set; }
    public string? AccountantMobile { get; set; }
    public string? CoordinatorMobile { get; set; }
    public string? Description { get; set; }
    [JsonConverter(typeof(CityEncryptedJsonConverter))]
    public int? CityId { get; set; }
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public bool CategoryIdsEdited { get; set; }
    [JsonConverter(typeof(CategoryIdListEncryptedJsonConverter))]
    public ICollection<int>? CategoryIds { get; set; }
    public bool ImportBrandsEdited { get; set; }
    [JsonConverter(typeof(BrandIdListEncryptedJsonConverter))]
    public List<int>? ImportBrands { get; set; }
    public bool ProduceBrandsEdited { get; set; }
    [JsonConverter(typeof(BrandIdListEncryptedJsonConverter))]
    public List<int>? ProduceBrands { get; set; }
    public bool SpreaderBrandsEdited { get; set; }
    [JsonConverter(typeof(BrandIdListEncryptedJsonConverter))]
    public List<int>? SpreaderBrands { get; set; }
    public bool? IsPerson { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? ImportDescription { get; set; }
    public string? ProduceDescription { get; set; }
    public string? SpreaderDescription { get; set; }
    public bool? HasImport { get; set; }
    public bool? HasProduce { get; set; }
    public bool? HasSpread { get; set; }
    public bool? Installments { get; set; }
    public bool? Cash { get; set; }
    public bool? PreOrder { get; set; }
    public string? NationalId { get; set; }
    public string? CompanyNationalId { get; set; }
}