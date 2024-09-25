using Application.DTOs.ClothesDTO;
using Application.Interfaces;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClothingAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClothesController : ControllerBase
    {
        private readonly IClothesService _clothesService;
        private readonly ILogger<ClothesController> _logger;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public ClothesController(IClothesService clothesService, ILogger<ClothesController> logger, IJwtTokenGenerator tokenGenerator)    
        {
            _clothesService = clothesService;
            _logger = logger;
            _tokenGenerator = tokenGenerator;
        }

        // Phương thức tạo mới Clothes
        [HttpPost]
        public async Task<IActionResult> CreateClothes([FromForm] CreateClothesDTO dto)
        {
            try
            {
                // Giả sử UserID được lấy từ hệ thống xác thực
                // Lấy token từ Authorization header
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                var userId = _tokenGenerator.GetUserIdFromToken(token);
                //var userId = "USER_01";
                await _clothesService.CreateClothesAsync(userId, dto);
                return Ok(new { message = "Clothes created successfully" });
            }
            /*
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            */
            catch (DbUpdateException ex) // Bắt lỗi liên quan đến việc lưu vào database
            {
                // Log lỗi để kiểm tra
                _logger.LogError(ex.InnerException?.Message ?? ex.Message);
                return BadRequest(new { message = ex.InnerException?.Message ?? "An error occurred while saving the entity changes." });
            }
            catch (Exception ex) // Bắt các lỗi chung khác
            {
                _logger.LogError(ex, "An error occurred");
                return BadRequest(new { message = "An unexpected error occurred." });
            }
        }

        // Phương thức cập nhật Clothes
        [HttpPut("{clothesId}")]
        public async Task<IActionResult> UpdateClothes(string clothesId, [FromBody] UpdateClothesDTO dto)
        {
            try
            {
                await _clothesService.UpdateClothesAsync(clothesId, dto);
                return Ok(new { message = "Clothes updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Phương thức xóa Clothes
        [HttpDelete("{clothesId}")]
        public async Task<IActionResult> DeleteClothes(string clothesId)
        {
            try
            {
                await _clothesService.DeleteClothesAsync(clothesId);
                return Ok(new { message = "Clothes deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Phương thức tìm kiếm nhiều Clothes theo CategoryIDs
        [HttpGet("search")]
        public async Task<IActionResult> SearchMultiple([FromQuery] List<string> categoryIds)
        {
            try
            {
                // Giả sử UserID được lấy từ hệ thống xác thực
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var clothesList = await _clothesService.SearchMultipleAsync(userId, categoryIds);
                return Ok(clothesList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Phương thức tìm kiếm một Clothes theo ClothesID
        [HttpGet("{clothesId}")]
        public async Task<IActionResult> SearchOne(string clothesId)
        {
            try
            {
                var clothes = await _clothesService.SearchOneAsync(clothesId);
                if (clothes == null)
                {
                    return NotFound(new { message = "Clothes not found" });
                }

                return Ok(clothes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}
