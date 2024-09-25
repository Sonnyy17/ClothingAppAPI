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
        Task CreateClothesAsync(string userId, CreateClothesDTO dto);
        Task UpdateClothesAsync(string clothesId, UpdateClothesDTO dto);
        Task DeleteClothesAsync(string clothesId);
        Task<List<ClothesResponseDTO>> SearchMultipleAsync(string userId, List<string> categoryIds);
        Task<ClothesResponseDTO> SearchOneAsync(string clothesId);
    }
}
