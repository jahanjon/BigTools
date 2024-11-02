using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Shopping
{
    public class ShoppingCartAddItemDto
    {
        public int CartId { get; set; }
        public int GoodId { get; set; }
        public int Quantity { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
