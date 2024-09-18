using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ClothesToCategory
    {
        public string ClothesID { get; set; }
        public string CategoryID { get; set; }

        public Clothes Clothes { get; set; }
        public ClothesCategory ClothesCategory { get; set; }
    }
}
