using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ClothesDTO
{
    public class SearchMultipleClothesRequestDTO
    {
        public List<string> CategoryIDs { get; set; }
    }
}
