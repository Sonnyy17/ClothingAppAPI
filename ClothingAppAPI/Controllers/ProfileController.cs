using Application.DTOs.ProfileDTO;
using Application.Interfaces;
using Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public ProfileController(IProfileService profileService, IJwtTokenGenerator tokenGenerator)
        {
            _profileService = profileService;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProfile([FromForm] CreateProfileDto dto)
        {
            var userId = GetUserIdFromToken(); // Hàm này lấy userId từ token trong Request
            var profileId = await _profileService.CreateProfileAsync(dto, userId);
            return Ok(new { ProfileID = profileId });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileDto dto)
        {
            var userId = GetUserIdFromToken(); // Hàm này lấy userId từ token trong Request
            var result = await _profileService.UpdateProfileAsync(dto, userId);
            return Ok(new { Success = result });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProfile()
        {
            var userId = GetUserIdFromToken(); // Hàm này lấy userId từ token trong Request
            var profile = await _profileService.SearchProfileAsync(userId);
            return Ok(profile);
        }

        [HttpGet("search/admin")]
        public async Task<IActionResult> SearchProfileForAdmin([FromQuery] string userId)
        {
            var profile = await _profileService.SearchProfileForAdminAsync(userId);
            return Ok(profile);
        }

        private string GetUserIdFromToken()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var userId = _tokenGenerator.GetUserIdFromToken(token);
            return userId;
        }
    }


}
