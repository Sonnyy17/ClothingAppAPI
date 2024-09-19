using Application.DTOs.RoleDTO;
using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<string> CreateRoleAsync(RoleCreateUpdateDTO roleDto)
        {
            // Sinh RoleId mới theo cấu trúc ROLE_1, ROLE_2...
            string newRoleId = await _roleRepository.GenerateNewRoleIdAsync();
            var newRole = new RoleDTO
            {
                RoleId = newRoleId,
                RoleName = roleDto.RoleName,
                Description = roleDto.Description
            };

            await _roleRepository.CreateAsync(newRole);
            return newRoleId;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<RoleDTO> GetRoleByIdAsync(string roleId)
        {
            return await _roleRepository.GetByIdAsync(roleId);
        }

        public async Task<bool> UpdateRoleAsync(string roleId, RoleCreateUpdateDTO roleDto)
        {
            var existingRole = await _roleRepository.GetByIdAsync(roleId);
            if (existingRole == null)
            {
                return false;
            }

            existingRole.RoleName = roleDto.RoleName;
            existingRole.Description = roleDto.Description;

            return await _roleRepository.UpdateAsync(existingRole);
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            return await _roleRepository.DeleteAsync(roleId);
        }
    }
}
