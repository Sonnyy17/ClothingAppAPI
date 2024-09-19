using Application.DTOs.ClothesCategoryDTO;
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
    public class ClothesCategoryRepository : IClothesCategoryRepository
    {
        private readonly AppDbContext _context;

        public ClothesCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ClothesCategoryDTO> CreateAsync(ClothesCategoryCreateUpdateDTO categoryDTO)
        {
            var category = new ClothesCategory
            {
                CategoryID = await GenerateNewCategoryIdAsync(), // Generate new ID
                CategoryName = categoryDTO.CategoryName,
                CategoryType = categoryDTO.CategoryType
            };

            _context.ClothesCategories.Add(category);
            await _context.SaveChangesAsync();

            return new ClothesCategoryDTO
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                CategoryType = category.CategoryType
            };
        }

        public async Task<ClothesCategoryDTO> GetByIdAsync(string categoryId)
        {
            var category = await _context.ClothesCategories
                .Where(c => c.CategoryID == categoryId)
                .Select(c => new ClothesCategoryDTO
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    CategoryType = c.CategoryType
                })
                .FirstOrDefaultAsync();

            return category;
        }

        public async Task<IEnumerable<ClothesCategoryDTO>> GetAllAsync()
        {
            return await _context.ClothesCategories
                .Select(c => new ClothesCategoryDTO
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    CategoryType = c.CategoryType
                })
                .ToListAsync();
        }

        public async Task UpdateAsync(string categoryId, ClothesCategoryCreateUpdateDTO categoryDTO)
        {
            var category = await _context.ClothesCategories.FindAsync(categoryId);
            if (category != null)
            {
                category.CategoryName = categoryDTO.CategoryName;
                category.CategoryType = categoryDTO.CategoryType;

                _context.ClothesCategories.Update(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(string categoryId)
        {
            var category = await _context.ClothesCategories.FindAsync(categoryId);
            if (category != null)
            {
                _context.ClothesCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ClothesCategoryDTO>> GetCategoryByTypeAsync(string categoryType)
        {
            return await _context.ClothesCategories
                .Where(c => c.CategoryType == categoryType)
                .Select(c => new ClothesCategoryDTO
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    CategoryType = c.CategoryType
                })
                .ToListAsync();
        }

        public async Task<string> GenerateNewCategoryIdAsync()
        {
            // Generate the next ID based on existing IDs
            var maxId = await _context.ClothesCategories
                .OrderByDescending(c => c.CategoryID)
                .Select(c => c.CategoryID)
                .FirstOrDefaultAsync();

            int nextId = 1;
            if (!string.IsNullOrEmpty(maxId))
            {
                nextId = int.Parse(maxId.Split('_').Last()) + 1;
            }

            return $"CA_{nextId}";
        }
    }
}
