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
            var lastRole = _context.Roles.OrderByDescending(r => r.RoleID).FirstOrDefault();
            int newId = lastRole == null ? 1 : int.Parse(lastRole.RoleID.Split('_')[1]) + 1;
            return $"ROLE_{newId}";
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
            return _context.Roles.Select(r => new RoleDTO
            {
                RoleId = r.RoleID,
                RoleName = r.RoleName,
                Description = r.Description
            }).ToList();
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

            entity.RoleName = role.RoleName;
            entity.Description = role.Description;
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
