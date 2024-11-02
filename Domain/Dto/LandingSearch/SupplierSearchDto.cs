namespace Domain.Dto.LandingSearch;

public class SupplierSearchDto
{
    public string Keyword { get; set; }
    public int CityId { get; set; }
    public int ProvinceId { get; set; }
    public int CategoryId { get; set; }
    public bool? IsImporter { get; set; }
    public bool? IsProducer { get; set; }
    public bool? IsSpreader { get; set; }
    public bool? CachePayment { get; set; }
    public bool? InstallmentPayment { get; set; }
    public int PaymentDuration { get; set; }
}