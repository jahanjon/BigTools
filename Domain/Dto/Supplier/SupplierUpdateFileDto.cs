using Common.Enums;

namespace Domain.Dto.Supplier;

public class SupplierUpdateFileDto
{
    public int SupplierId { get; set; }
    public Guid FileId { get; set; }
    public FileType FileType { get; set; }
}