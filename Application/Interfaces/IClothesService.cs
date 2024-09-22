using Application.DTOs.ClothesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClothesService
    {
        Task<ClothesDTO> CreateClothesAsync(ClothesCreateUpdateDTO clothesDto, string userId);
        Task<List<ClothesDTO>> GetAllClothesByUserAsync(string userId);
        Task<ClothesDTO> GetClothesByIdAsync(string clothesId);
    }
}
