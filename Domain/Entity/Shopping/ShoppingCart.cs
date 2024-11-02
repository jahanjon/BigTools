using Domain.Entity.Base;
using Domain.Entity.Profile;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Shopping
{
    public class ShoppingCart : BaseEntity
    {
        public int ShopperId { get; set; }
        public Shopper Shopper { get; set; }
        public DateTime CreatedDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(sc => sc.Id); 
            builder.Property(sc => sc.CreatedDate).IsRequired();
            builder.HasOne(sc => sc.Shopper).WithMany(s => s.ShoppingCarts);
            builder.HasMany(sc => sc.CartItems).WithOne(ci => ci.ShoppingCart); 
        }
    }
}
