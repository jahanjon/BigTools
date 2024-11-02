using Domain.Entity.Base;
using Domain.Entity.Profile;

namespace Domain.Entity.Product;

public class Brand : BaseEntity
{
    public string Name { get; set; }
    public string EnName { get; set; }
    public int? OwnerId { get; set; }
    public Supplier? Owner { get; set; }
    public Guid LogoFileGuid { get; set; }
    public Guid? PriceListGuid { get; set; }
    public ICollection<Good> Goods { get; set; }
}