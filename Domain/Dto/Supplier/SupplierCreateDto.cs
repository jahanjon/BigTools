namespace Domain.Dto.Supplier;

public class SupplierCreateDto
{
    public string Name { get; set; }
    //public string LastName { get; set; }
    public string Phone { get; set; }
    public string Mobile { get; set; }
    //public string ManagerMobile { get; set; }
    public string AccountantMobile { get; set; }
    public string CoordinatorMobile { get; set; }
    public string Description { get; set; }
    public int CityId { get; set; }
    public string Address { get; set; }
    public ICollection<int> CategoryIds { get; set; }
    public List<int> Import { get; set; }
    public List<int> Produce { get; set; }
    public List<int> Spreader { get; set; }
    public bool IsPerson { get; set; }
    public string ImportDescription { get; set; }
    public string ProduceDescription { get; set; }
    public string SpreaderDescription { get; set; }
    public bool HasImport { get; set; }
    public bool HasProduce { get; set; }
    public bool HasSpread { get; set; }
    public bool Installments { get; set; }
    public int InstallmentsDays { get; set; }
    public bool Cash { get; set; }
    public int CashDays { get; set; }
    public bool PreOrder { get; set; }
    public string NationalId { get; set; }
}