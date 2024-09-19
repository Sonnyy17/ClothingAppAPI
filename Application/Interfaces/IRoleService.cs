using Application.DTOs.RoleDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        Task<string> CreateRoleAsync(RoleCreateUpdateDTO roleDto);
        Task<IEnumerable<RoleDTO>> GetAllRolesAsync();
        Task<RoleDTO> GetRoleByIdAsync(string roleId);
        Task<bool> UpdateRoleAsync(string roleId, RoleCreateUpdateDTO roleDto);
        Task<bool> DeleteRoleAsync(string roleId);
    }
}
