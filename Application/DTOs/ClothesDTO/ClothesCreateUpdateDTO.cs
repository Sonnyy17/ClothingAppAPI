﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ClothesDTO
{
    public class ClothesCreateUpdateDTO
    {
        public string ClothesName { get; set; }
        public string Description { get; set; }
        public List<string> CategoryIDs { get; set; }
        public IFormFile ImageFile { get; set; } 
    }

}