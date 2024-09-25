using Application.DTOs.ClothesDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IClothesRepository
    {
        Task<string> GenerateClothesIDAsync(string userId);
        Task CreateClothesAsync(Clothes clothes);
        Task AddImageAsync(string clothesId, string imagePath);
        Task AddCategoriesAsync(string clothesId, List<string> categoryIds);
        Task DeleteCategoriesAsync(string clothesId);
        Task UpdateClothesAsync(Clothes clothes);
        Task DeleteClothesAsync(string clothesId);
        Task<List<Clothes>> SearchMultipleAsync(string userId, List<string> categoryIds);
        Task<Clothes> SearchOneAsync(string clothesId);

        // Thêm phương thức để lấy CategoryIDs theo ClothesID
        Task<List<string>> GetCategoryIdsForClothesAsync(string clothesId);

        Task<string> GetImagePathAsync(string clothesId);
    }

}
