using Common.Enums;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Shopping
{
    public class ShoppingCartCreateDto
    {
        public int ShopperId { get; set; }
        public PaymentMethod PaymentType { get; set; }
    }
}
