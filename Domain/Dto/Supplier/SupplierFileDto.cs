namespace Domain.Dto.Supplier;

public class SupplierFileDto
{
    public int SupplierId { get; set; }
    public List<Guid> FileIds { get; set; }
}