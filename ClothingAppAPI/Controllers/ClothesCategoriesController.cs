using Application.DTOs.ClothesCategoryDTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothesCategoriesController : ControllerBase
    {
        private readonly IClothesCategoryService _service;

        public ClothesCategoriesController(IClothesCategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClothesCategoryCreateUpdateDTO categoryDTO)
        {
            var result = await _service.CreateAsync(categoryDTO);
            return CreatedAtAction(nameof(GetById), new { id = result.CategoryID }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound(new { message = "Category not found." });
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ClothesCategoryCreateUpdateDTO categoryDTO)
        {
            try
            {
                await _service.UpdateAsync(id, categoryDTO);
                return Ok(new { message = "Category updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok(new { message = "Category deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("by-type/{categoryType}")]
        public async Task<IActionResult> GetByType(string categoryType)
        {
            var result = await _service.GetCategoryByTypeAsync(categoryType);
            return Ok(result);
        }
    }

}
