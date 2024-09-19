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
            if (result == null) return NotFound();
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
            await _service.UpdateAsync(id, categoryDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("by-type/{categoryType}")]
        public async Task<IActionResult> GetByType(string categoryType)
        {
            var result = await _service.GetCategoryByTypeAsync(categoryType);
            return Ok(result);
        }
    }
}
