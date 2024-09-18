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
        public string ClothesID { get; set; }
        public string UserID { get; set; }
        public string ClothesName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public UserAccount UserAccount { get; set; }
        public ICollection<ClothesImage> ClothesImages { get; set; }
        public ICollection<ClothesToCategory> ClothesToCategories { get; set; }
    }
}
