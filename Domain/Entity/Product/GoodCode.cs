using Domain.Entity.Base;
using Domain.Entity.Profile;

namespace Domain.Entity.Product;

public class GoodCode : BaseEntity
{
    public string Code { get; set; }
    public Supplier Supplier { get; set; }
    public int SupplierId { get; set; }
    public Good Good { get; set; }
    public int GoodId { get; set; }
}