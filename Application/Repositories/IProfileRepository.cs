using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IProfileRepository
    {
        Task<UserProfile> GetProfileByUserIdAsync(string userId);
        Task<UserProfile> GetProfileByIdAsync(string profileId);
        Task<UserAccount> GetUserAccountByUserIdAsync(string userId);
        Task<Role> GetRoleByRoleIdAsync(string roleId);
        Task CreateProfileAsync(UserProfile profile);
        Task UpdateProfileAsync(UserProfile profile);
        Task<bool> ProfileExistsAsync(string userId);
    }



}
