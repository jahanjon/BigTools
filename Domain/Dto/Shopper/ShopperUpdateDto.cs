namespace Domain.Dto.Shopper;

public class ShopperUpdateDto
{
    public int Id { get; set; }
    public string? PersonName { get; set; }
    public string? Phone { get; set; }
    public string? NationCode { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? HomeCityId { get; set; }
    public string? HomePostalCode { get; set; }
    public string? HomeAddress { get; set; }
    public string? Name { get; set; }
    public int? CityId { get; set; }
    public string? PostalCode { get; set; }
    public string? Address { get; set; }
    public bool? IsRent { get; set; }
    public int? Area { get; set; }
    public bool? HasLicense { get; set; }
    public string? LicenseCode { get; set; }
    public bool? IsRetail { get; set; }
    public Guid? LicenseImage { get; set; }
    public bool DocOrRentImagesEdited { get; set; }
    public List<Guid>? DocOrRentImages { get; set; }
    public bool CategoryIdsEdited { get; set; }
    public List<int>? CategoryIds { get; set; }
    public bool BrandIdsEdited { get; set; }
    public List<int>? BrandIds { get; set; }
}