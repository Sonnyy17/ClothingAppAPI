using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ClothesCategory
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }

        public ICollection<ClothesToCategory> ClothesToCategories { get; set; }
    }
}
