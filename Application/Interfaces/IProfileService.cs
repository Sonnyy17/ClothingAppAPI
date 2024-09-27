using Application.DTOs.ProfileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProfileService
    {
        Task<string> CreateProfileAsync(CreateProfileDto dto, string userId);
        Task<bool> UpdateProfileAsync(UpdateProfileDto dto, string userId);
        Task<ProfileResponseDto> SearchProfileAsync(string userId);
        Task<ProfileResponseDto> SearchProfileForAdminAsync(string userId);
    }


}
