using System.ComponentModel.DataAnnotations;
using Domain.Entity.Base;
using Domain.Entity.Category;
using Domain.Entity.Identity;
using Domain.Entity.Place;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Profile;

public class Supplier : BaseEntity
{
    public string Name { get; set; }
    public string LastName { get; set; }

    [RegularExpression("^0[0-9]{2,}[0-9]{7,}$", ErrorMessage = "Invalid phone number.")]
    public string Phone { get; set; }
    public string Phone2 { get; set; }
    public string Mobile { get; set; }
    public string ManagerMobile { get; set; }
    public string AccountantMobile { get; set; }
    public string CoordinatorMobile { get; set; }
    public string NationalId { get; set; }
    public string CompanyNationalId { get; set; }
    public string Description { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public bool IsPerson { get; set; }
    public DateTime? BirthDate { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<CategoryLevel1> Categories { get; set; }
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
}

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Property(p => p.LastName);
        builder.HasOne(s => s.City);
        builder.HasOne(s => s.User);
        builder.HasMany(s => s.Categories).WithMany(x => x.Suppliers);
        builder.Property(s => s.NationalId).IsRequired().HasMaxLength(11);
    }
}