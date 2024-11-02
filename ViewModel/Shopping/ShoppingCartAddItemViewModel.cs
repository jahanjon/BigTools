using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Shopping
{
    public class ShoppingCartAddItemViewModel
    {
        public int GoodId { get; set; }
        public int Quantity { get; set; }
        public PaymentType PaymentType { get; set; } 
    }
}
