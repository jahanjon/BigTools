using Domain.Entity.Base;
using Domain.Entity.Category;
using Domain.Entity.Financial;

namespace Domain.Entity.Product;

public class Good : BaseEntity
{
    public string Name { get; set; }
    public Brand? Brand { get; set; }
    public int? BrandId { get; set; }
    public CategoryLevel3 Category { get; set; }
    public int CategoryId { get; set; }
    public string SystemCode { get; set; }
    public int PackageTypeId { get; set; }
    public PackageType PackageType { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
    public int CountInBox { get; set; }
    public string Description { get; set; }
    public ICollection<GoodCode> GoodCodes { get; set; }
    public ICollection<Price> Prices { get; set; }
}