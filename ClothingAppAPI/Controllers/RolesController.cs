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
            return Ok(new { RoleId = roleId });
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
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var success = await _roleService.DeleteRoleAsync(roleId);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
