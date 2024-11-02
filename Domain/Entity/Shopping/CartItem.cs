using Domain.Entity.Base;
using Domain.Entity.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Shopping
{
    public class CartItem : BaseEntity
    {
        public int ShoppingCartId { get; set; } 
        public ShoppingCart ShoppingCart { get; set; }
        public int GoodId { get; set; }
        public Good Good { get; set; } 
        public int Quantity { get; set; } 
    }
}
