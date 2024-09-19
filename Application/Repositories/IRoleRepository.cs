using Application.DTOs.RoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IRoleRepository
    {
        Task<string> GenerateNewRoleIdAsync();
        Task CreateAsync(RoleDTO role);
        Task<IEnumerable<RoleDTO>> GetAllAsync();
        Task<RoleDTO> GetByIdAsync(string roleId);
        Task<bool> UpdateAsync(RoleDTO role);
        Task<bool> DeleteAsync(string roleId);
    }
}
