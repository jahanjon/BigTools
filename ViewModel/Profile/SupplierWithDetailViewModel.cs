using Newtonsoft.Json;
using ViewModel.Category;
using ViewModel.EncryptedJsonConverters.Place;
using ViewModel.EncryptedJsonConverters.Profile;
using ViewModel.File;
using ViewModel.Financial;
using ViewModel.Product;

namespace ViewModel.Profile;

public class SupplierWithDetailViewModel
{
    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int Id { get; set; }
    public string Name { get; set; }
    //public string LastName { get; set; }
    [JsonConverter(typeof(CityEncryptedJsonConverter))]
    public int CityId { get; set; }
    public string CityName { get; set; }
    [JsonConverter(typeof(ProvinceEncryptedJsonConverter))]
    public int ProvinceId { get; set; }
    public string ProvinceName { get; set; }
    public List<CategoryKeyValueViewModel> Categories { get; set; }
    public List<FileViewModel> Attachments { get; set; }
    public bool IsImporter { get; set; }
    public bool IsProducer { get; set; }
    public bool IsSpreader { get; set; }
    public SupplyDetailsViewModel ImportDetails { get; set; }
    public SupplyDetailsViewModel ProduceDetails { get; set; }
    public SupplyDetailsViewModel SpreadDetails { get; set; }

    public string Phone { get; set; }
    public string Phone2 { get; set; }
    public string Mobile { get; set; }
    public string ManagerMobile { get; set; }
    public string AccountantMobile { get; set; }
    public string CoordinatorMobile { get; set; }
    public string NationalId { get; set; }
    public string CompanyNationalId { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public bool IsPerson { get; set; }
    public DateTime? BirthDate { get; set; }
    public bool Installments { get; set; }
    public bool Cash { get; set; }
    public bool PreOrder { get; set; }
    public bool IsActive { get; set; }
    public List<int> PaymentDurationDaysList { get; set; }
}

public class SupplyDetailsViewModel
{
    public List<CategoryKeyValueViewModel> Categories { get; set; }
    public List<BrandSummaryViewModel> Brands { get; set; }
    //public List<GoodViewModel> Goods { get; set; }
    public List<GoodDiscountSummaryViewModel> GoodDiscounts { get; set; }
    public string Description { get; set; }
}