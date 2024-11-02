using Domain.Dto.Category;
using Domain.Dto.File;
using Domain.Dto.Financial;
using Domain.Dto.Product;

namespace Domain.Dto.Supplier;

public class SupplierWithDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    //public string LastName { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; }
    public int ProvinceId { get; set; }
    public string ProvinceName { get; set; }
    public List<CategoryKeyValueDto> Categories { get; set; }
    public List<FileDto> Attachments { get; set; }
    public bool IsImporter { get; set; }
    public bool IsProducer { get; set; }
    public bool IsSpreader { get; set; }
    public SupplyDetailsDto ImportDetails { get; set; }
    public SupplyDetailsDto ProduceDetails { get; set; }
    public SupplyDetailsDto SpreadDetails { get; set; }

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

public class SupplyDetailsDto
{
    public List<CategoryKeyValueDto> Categories { get; set; }
    public List<BrandSummaryDto> Brands { get; set; }
    //public List<GoodDto> Goods { get; set; }
    public List<GoodDiscountSummaryDto> GoodDiscounts { get; set; }
    public string Description { get; set; }
}