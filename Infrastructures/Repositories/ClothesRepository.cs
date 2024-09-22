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

        public async Task<int> GetUserClothesCountAsync(string userId)
        {
            return await _context.Clothes.CountAsync(c => c.UserID == userId);
        }

        public async Task CreateClothesAsync(Clothes clothes, List<ClothesToCategory> categories, ClothesImage image)
        {
            await _context.Clothes.AddAsync(clothes);
            await _context.ClothesToCategories.AddRangeAsync(categories);
            await _context.ClothesImages.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Clothes>> GetClothesByUserAsync(string userId)
        {
            return await _context.Clothes
                .Include(c => c.ClothesToCategories)
                .Include(c => c.ClothesImages)
                .Where(c => c.UserID == userId)
                .ToListAsync();
        }

        public async Task<Clothes> GetClothesByIdAsync(string clothesId)
        {
            return await _context.Clothes
                .Include(c => c.ClothesToCategories)
                .Include(c => c.ClothesImages)
                .FirstOrDefaultAsync(c => c.ClothesID == clothesId);
        }
    }

}
