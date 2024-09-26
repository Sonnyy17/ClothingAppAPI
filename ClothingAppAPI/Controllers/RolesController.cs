using Application.DTOs.RoleDTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClothingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateUpdateDTO roleDto)
        {
            var roleId = await _roleService.CreateRoleAsync(roleDto);
            var createdRole = await _roleService.GetRoleByIdAsync(roleId); // Lấy thông tin Role mới tạo
            return CreatedAtAction(nameof(GetRoleById), new { roleId = createdRole.RoleId }, createdRole); // Trả về RoleDTO
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet("{roleId}")]
        public async Task<ActionResult<RoleDTO>> GetRoleById(string roleId)
        {
            var role = await _roleService.GetRoleByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateRole(string roleId, RoleCreateUpdateDTO roleDto)
        {
            var success = await _roleService.UpdateRoleAsync(roleId, roleDto);
            if (!success)
            {
                return NotFound("Role ID không tồn tại."); // Thông báo rõ ràng
            }
            var updatedRole = await _roleService.GetRoleByIdAsync(roleId); // Lấy Role đã cập nhật
            return Ok(new { Message = "Cập nhật thành công.", Role = updatedRole }); // Trả về Role đã cập nhật
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var success = await _roleService.DeleteRoleAsync(roleId);
            if (!success)
            {
                return NotFound("Role ID không tồn tại."); // Thông báo rõ ràng
            }
            return Ok(new { Message = "Xóa thành công." }); // Thông báo khi xóa thành công
        }
    }

}
