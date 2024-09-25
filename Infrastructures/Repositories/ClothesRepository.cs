using Application.DTOs.ClothesDTO;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ClothesRepository : IClothesRepository
    {
        private readonly AppDbContext _context;

        public ClothesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateClothesIDAsync(string userId)
        {
            return userId + DateTime.Now.ToString("ddMMyyyyHHmmss");
        }

        public async Task CreateClothesAsync(Clothes clothes)
        {
            _context.Clothes.Add(clothes);
            await _context.SaveChangesAsync();
        }

        public async Task AddImageAsync(string clothesId, string imagePath)
        {
            var clothesImage = new ClothesImage
            {
                ImageID = clothesId,
                ClothesID = clothesId,
                ImagePath = imagePath,
                UploadedDate = DateTime.Now
            };
            _context.ClothesImages.Add(clothesImage);
            await _context.SaveChangesAsync();
        }

        public async Task AddCategoriesAsync(string clothesId, List<string> categoryIds)
        {
            foreach (var categoryId in categoryIds)
            {
                var clothesToCategory = new ClothesToCategory
                {
                    ClothesID = clothesId,
                    CategoryID = categoryId
                };
                _context.ClothesToCategories.Add(clothesToCategory);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoriesAsync(string clothesId)
        {
            var categories = _context.ClothesToCategories.Where(c => c.ClothesID == clothesId);
            _context.ClothesToCategories.RemoveRange(categories);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClothesAsync(Clothes clothes)
        {
            _context.Clothes.Update(clothes);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClothesAsync(string clothesId)
        {
            var clothes = await _context.Clothes.FindAsync(clothesId);
            if (clothes != null)
            {
                _context.Clothes.Remove(clothes);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Clothes>> SearchMultipleAsync(string userId, List<string> categoryIds)
        {
            return await _context.Clothes
                .Where(c => c.UserID == userId &&
                            _context.ClothesToCategories.Any(ctc => categoryIds.Contains(ctc.CategoryID) && ctc.ClothesID == c.ClothesID))
                .ToListAsync();
        }

        public async Task<Clothes> SearchOneAsync(string clothesId)
        {
            return await _context.Clothes.FirstOrDefaultAsync(c => c.ClothesID == clothesId);
        }

        // Lấy danh sách CategoryIDs cho Clothes
        public async Task<List<string>> GetCategoryIdsForClothesAsync(string clothesId)
        {
            return await _context.ClothesToCategories
                .Where(c => c.ClothesID == clothesId)
                .Select(c => c.CategoryID)
                .ToListAsync();
        }

        public async Task<string> GetImagePathAsync(string clothesId)
        {
            var image = await _context.ClothesImages.FirstOrDefaultAsync(c => c.ClothesID == clothesId);
            return image?.ImagePath;
        }
    }

}
