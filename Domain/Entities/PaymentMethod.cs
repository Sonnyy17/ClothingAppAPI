using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PaymentMethod
    {
        public string PaymentMethodID { get; set; }
        public string MethodName { get; set; }
        public string Description { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
