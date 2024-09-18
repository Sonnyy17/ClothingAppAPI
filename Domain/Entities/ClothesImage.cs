using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ClothesImage
    {
        public string ImageID { get; set; }
        public string ClothesID { get; set; }
        public string ImagePath { get; set; }
        public DateTime UploadedDate { get; set; }

        public Clothes Clothes { get; set; }
    }
}
