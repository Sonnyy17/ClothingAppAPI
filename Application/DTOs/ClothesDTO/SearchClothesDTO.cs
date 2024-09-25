using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ClothesDTO
{
    public class SearchClothesDTO
    {
        public string ClothesID { get; set; }
        public string ClothesName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}
