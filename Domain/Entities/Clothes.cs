using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Clothes
    {
        [Key]
        public string ClothesID { get; set; }
        public int UserID { get; set; }
        public string ClothesName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
