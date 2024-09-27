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
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context; // Inject DbContext

        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> GetProfileByUserIdAsync(string userId)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserID == userId);
        }

        public async Task<UserProfile> GetProfileByIdAsync(string profileId)
        {
            return await _context.UserProfiles.FindAsync(profileId);
        }

        public async Task<UserAccount> GetUserAccountByUserIdAsync(string userId)
        {
            return await _context.UserAccounts.FindAsync(userId);
        }

        public async Task<Role> GetRoleByRoleIdAsync(string roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task CreateProfileAsync(UserProfile profile)
        {
            await _context.UserProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProfileAsync(UserProfile profile)
        {
            _context.UserProfiles.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ProfileExistsAsync(string userId)
        {
            return await _context.UserProfiles.AnyAsync(p => p.UserID == userId);
        }
    }


}
