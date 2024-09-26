using Application.DTOs.RoleDTO;
using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateNewRoleIdAsync()
        {
            var count = await _context.Roles.CountAsync();
            return $"ROLE_{count + 1}";
        }

        public async Task CreateAsync(RoleDTO role)
        {
            var entity = new Role
            {
                RoleID = role.RoleId,
                RoleName = role.RoleName,
                Description = role.Description
            };

            _context.Roles.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            return await _context.Roles.Select(r => new RoleDTO
            {
                RoleId = r.RoleID,
                RoleName = r.RoleName,
                Description = r.Description
            }).ToListAsync();
        }

        public async Task<RoleDTO> GetByIdAsync(string roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role == null) return null;

            return new RoleDTO
            {
                RoleId = role.RoleID,
                RoleName = role.RoleName,
                Description = role.Description
            };
        }

        public async Task<bool> UpdateAsync(RoleDTO role)
        {
            var entity = await _context.Roles.FindAsync(role.RoleId);
            if (entity == null) return false;

            // Chỉ cập nhật nếu RoleName và Description có giá trị mới
            if (!string.IsNullOrEmpty(role.RoleName))
            {
                entity.RoleName = role.RoleName;
            }
            if (!string.IsNullOrEmpty(role.Description))
            {
                entity.Description = role.Description;
            }

            _context.Roles.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string roleId)
        {
            var entity = await _context.Roles.FindAsync(roleId);
            if (entity == null) return false;

            _context.Roles.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
