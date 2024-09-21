using Application.DTOs.AccountDTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountsController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;

        public UserAccountsController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(string userId)
        {
            var user = await _userAccountService.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User không tồn tại.");
            }

            return Ok(user);
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetByUserName(string username)
        {
            var user = await _userAccountService.GetByUsernameAsync(username);
            if (user == null)
            {
                return NotFound("User không tồn tại.");
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userAccountService.GetAllAsync();
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterAccountDTO registerDTO)
        {
            try
            {
                var newUser = await _userAccountService.RegisterAsync(registerDTO);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginAccountDTO loginDTO)
        {
            try
            {
                var user = await _userAccountService.LoginAsync(loginDTO);
                return Ok("Đăng nhập thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAccount(string userId ,UpdateAccountDTO updateDTO)
        {
            try
            {
                var user = await _userAccountService.UpdateAsync(userId,updateDTO);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{userId}/role")]
        public async Task<IActionResult> UpdateRoleAccount(string userId, [FromBody] UpdateRoleAccountDTO updateRoleDTO)
        {
            try
            {
                var updatedUser = await _userAccountService.UpdateRoleAccountAsync(userId, updateRoleDTO);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteById(string userId)
        {
            try
            {
                await _userAccountService.DeleteByIdAsync(userId);
                return Ok(new { message = "Xóa tài khoản thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}
