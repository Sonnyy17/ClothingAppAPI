using Application.DTOs.ProfileDTO;
using Application.Interfaces;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public ProfileService(IProfileRepository profileRepository, ICloudinaryService cloudinaryService)
        {
            _profileRepository = profileRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<string> CreateProfileAsync(CreateProfileDto dto, string userId)
        {
            if (await _profileRepository.ProfileExistsAsync(userId))
                throw new Exception("Người dùng đã có profile.");

            // Upload hình ảnh lên Cloudinary
            var profilePictureUrl = dto.ProfilePicture != null ? await _cloudinaryService.UploadImageAsync(dto.ProfilePicture) : null;

            var profile = new UserProfile
            {
                ProfileID = $"Profile_{userId}",
                UserID = userId,
                FullName = dto.FullName,
                Gender = dto.Gender,
                BirthDate = dto.BirthDate,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                ProfilePicture = profilePictureUrl
            };

            await _profileRepository.CreateProfileAsync(profile);
            return profile.ProfileID;
        }

        public async Task<bool> UpdateProfileAsync(UpdateProfileDto dto, string userId)
        {
            var profile = await _profileRepository.GetProfileByUserIdAsync(userId);
            if (profile == null)
                throw new Exception("Profile không tồn tại.");

            // Cập nhật thông tin
            profile.FullName = dto.FullName ?? profile.FullName;
            profile.Gender = dto.Gender ?? profile.Gender;
            profile.BirthDate = dto.BirthDate ?? profile.BirthDate;
            profile.PhoneNumber = dto.PhoneNumber ?? profile.PhoneNumber;
            profile.Address = dto.Address ?? profile.Address;

            if (dto.ProfilePicture != null)
            {
                // Xóa hình cũ nếu có
                if (!string.IsNullOrEmpty(profile.ProfilePicture))
                    await _cloudinaryService.DeleteImageAsync(profile.ProfilePicture);

                // Upload hình mới
                profile.ProfilePicture = await _cloudinaryService.UploadImageAsync(dto.ProfilePicture);
            }

            await _profileRepository.UpdateProfileAsync(profile);
            return true;
        }

        public async Task<ProfileResponseDto> SearchProfileAsync(string userId)
        {
            var profile = await _profileRepository.GetProfileByUserIdAsync(userId);
            var userAccount = await _profileRepository.GetUserAccountByUserIdAsync(userId);
            var role = await _profileRepository.GetRoleByRoleIdAsync(userAccount.RoleID);

            return new ProfileResponseDto
            {
                FullName = profile.FullName,
                Gender = profile.Gender,
                BirthDate = profile.BirthDate,
                PhoneNumber = profile.PhoneNumber,
                Address = profile.Address,
                ProfilePicture = profile.ProfilePicture,
                UserName = userAccount.Username,
                CreateDate = userAccount.CreatedDate,
                //tesst
                LastLoginDate = (DateTime)userAccount.LastLoginDate,
                RoleName = role.RoleName
            };
        }

        public async Task<ProfileResponseDto> SearchProfileForAdminAsync(string userId)
        {
            var profile = await _profileRepository.GetProfileByUserIdAsync(userId);
            if (profile == null)
                throw new Exception("Profile không tồn tại.");

            var userAccount = await _profileRepository.GetUserAccountByUserIdAsync(userId);
            var role = await _profileRepository.GetRoleByRoleIdAsync(userAccount.RoleID);

            return new ProfileResponseDto
            {
                FullName = profile.FullName,
                Gender = profile.Gender,
                BirthDate = profile.BirthDate,
                PhoneNumber = profile.PhoneNumber,
                Address = profile.Address,
                ProfilePicture = profile.ProfilePicture,
                UserName = userAccount.Username,
                CreateDate = userAccount.CreatedDate,
                LastLoginDate = (DateTime)userAccount.LastLoginDate,
                RoleName = role.RoleName
            };
        }
    }

}
