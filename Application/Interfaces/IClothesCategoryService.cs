using Application.DTOs.ClothesCategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClothesCategoryService
    {
        Task<ClothesCategoryDTO> CreateAsync(ClothesCategoryCreateUpdateDTO categoryDTO);
        Task<ClothesCategoryDTO> GetByIdAsync(string categoryId);
        Task<IEnumerable<ClothesCategoryDTO>> GetAllAsync();
        Task UpdateAsync(string categoryId, ClothesCategoryCreateUpdateDTO categoryDTO);
        Task DeleteAsync(string categoryId);
        Task<IEnumerable<ClothesCategoryDTO>> GetCategoryByTypeAsync(string categoryType);
    }
}
