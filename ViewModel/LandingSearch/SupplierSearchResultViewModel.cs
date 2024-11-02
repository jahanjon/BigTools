using Domain.Dto.Category;
using Domain.Dto.File;
using Domain.Dto.Financial;
using Newtonsoft.Json;
using ViewModel.EncryptedJsonConverters.Profile;

namespace ViewModel.LandingSearch;

public class SupplierSearchResultViewModel
{
    [JsonConverter(typeof(SupplierEncryptedJsonConverter))]
    public int Id { get; set; }
    public string Name { get; set; }
    public FileDto Image { get; set; }
    public string CityName { get; set; }
    public string ProvinceName { get; set; }
    public List<CategoryKeyValueDto> Categories { get; set; }
    public bool IsImporter { get; set; }
    public bool IsProducer { get; set; }
    public bool IsSpreader { get; set; }
    public List<GoodDiscountKeyValueDto> GoodDiscounts { get; set; }
}