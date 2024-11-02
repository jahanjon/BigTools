using Domain.Entity.Base;
using Domain.Entity.Product;
using Domain.Entity.Profile;

namespace Domain.Entity.Financial;

public class Price : BaseEntity
{
    public decimal Amount { get; set; }
    public Supplier Supplier { get; set; }
    public int SupplierId { get; set; }
    public Good Good { get; set; }
    public int GoodId { get; set; }
    public int Count { get; set; }
}