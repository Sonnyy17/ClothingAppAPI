using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment
    {
        public string PaymentID { get; set; }
        public string UserID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethodID { get; set; }
        public string Status { get; set; }

        public UserAccount UserAccount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
