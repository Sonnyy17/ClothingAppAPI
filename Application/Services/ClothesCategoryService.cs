using Application.DTOs.ClothesCategoryDTO;
using Application.Interfaces;
using Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClothesCategoryService : IClothesCategoryService
    {
        private readonly IClothesCategoryRepository _repository;

        public ClothesCategoryService(IClothesCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClothesCategoryDTO> CreateAsync(ClothesCategoryCreateUpdateDTO categoryDTO)
        {
            return await _repository.CreateAsync(categoryDTO);
        }

        public async Task<ClothesCategoryDTO> GetByIdAsync(string categoryId)
        {
            return await _repository.GetByIdAsync(categoryId);
        }

        public async Task<IEnumerable<ClothesCategoryDTO>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateAsync(string categoryId, ClothesCategoryCreateUpdateDTO categoryDTO)
        {
            // Xử lý cập nhật và thông báo
            var existingCategory = await _repository.GetByIdAsync(categoryId);
            if (existingCategory == null)
            {
                throw new Exception("Category not found.");
            }

            await _repository.UpdateAsync(categoryId, categoryDTO);
        }

        public async Task DeleteAsync(string categoryId)
        {
            // Kiểm tra tồn tại và xóa
            var existingCategory = await _repository.GetByIdAsync(categoryId);
            if (existingCategory == null)
            {
                throw new Exception("Category not found.");
            }

            await _repository.DeleteAsync(categoryId);
        }

        public async Task<IEnumerable<ClothesCategoryDTO>> GetCategoryByTypeAsync(string categoryType)
        {
            return await _repository.GetCategoryByTypeAsync(categoryType);
        }
    }

}
