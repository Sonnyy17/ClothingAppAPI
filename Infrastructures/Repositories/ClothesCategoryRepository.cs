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
                // Kiểm tra và cập nhật chỉ các trường có dữ liệu mới
                if (!string.IsNullOrEmpty(categoryDTO.CategoryName))
                    category.CategoryName = categoryDTO.CategoryName;

                if (!string.IsNullOrEmpty(categoryDTO.CategoryType))
                    category.CategoryType = categoryDTO.CategoryType;

                _context.ClothesCategories.Update(category);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Category not found.");
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
            else
            {
                throw new Exception("Category not found.");
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
            /*var lastCategory = await _context.ClothesCategories
                                             .OrderByDescending(c => c.CategoryID)
                                             .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (lastCategory != null)
            {
                if (int.TryParse(lastCategory.CategoryID, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return nextNumber.ToString().PadLeft(2, '0');*/
            var count = await _context.ClothesCategories.CountAsync();
            return $"CA_{count + 1}";
        }



    }

}
