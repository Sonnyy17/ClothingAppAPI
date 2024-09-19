using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PaymentMethodDTO
{
    public class PaymentMethodCreateUpdateDTO
    {
        public string PaymentMethodName { get; set; }
        public string Description { get; set; }
    }
}
