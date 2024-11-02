using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Shopping
{
    public class ShoppingCartCreateViewModel
    {
        public int ShopperId { get; set; }
        public PaymentType PaymentType { get; set; } 
    }
}
