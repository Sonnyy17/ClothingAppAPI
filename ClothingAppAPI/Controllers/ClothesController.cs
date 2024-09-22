using Application.DTOs.ClothesDTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClothingAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClothesController : ControllerBase
    {
        private readonly IClothesService _clothesService;

        public ClothesController(IClothesService clothesService)
        {
            _clothesService = clothesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClothes([FromForm] ClothesCreateUpdateDTO clothesDto)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Assume user is authenticated
            var userId = "USER_01";

            var createdClothes = await _clothesService.CreateClothesAsync(clothesDto, userId);
            return Ok(createdClothes);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetClothesByUser(string userId)
        {
            var clothes = await _clothesService.GetAllClothesByUserAsync(userId);
            return Ok(clothes);
        }

        [HttpGet("clothes/{clothesId}")]
        public async Task<IActionResult> GetClothesById(string clothesId)
        {
            var clothes = await _clothesService.GetClothesByIdAsync(clothesId);
            return clothes == null ? NotFound() : Ok(clothes);
        }
    }

}
