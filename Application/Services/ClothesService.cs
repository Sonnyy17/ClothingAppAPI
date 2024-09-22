using Application.DTOs.ClothesDTO;
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
    public class ClothesService : IClothesService
    {
        private readonly IClothesRepository _clothesRepository;
        private readonly ICloudinaryService _cloudinaryService; // Assume CloudinaryService is implemented

        public ClothesService(IClothesRepository clothesRepository, ICloudinaryService cloudinaryService)
        {
            _clothesRepository = clothesRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ClothesDTO> CreateClothesAsync(ClothesCreateUpdateDTO clothesDto, string userId)
        {
            // Generate ClothesID (e.g., userId_001)
            var clothesCount = await _clothesRepository.GetUserClothesCountAsync(userId);
            var clothesId = $"{userId}_{clothesCount + 1}";

            // Upload image to Cloudinary
            var imagePath = await _cloudinaryService.UploadImageAsync(clothesDto.ImageFile);

            // Create Clothes entity
            var clothes = new Clothes
            {
                ClothesID = clothesId,
                UserID = userId,
                ClothesName = clothesDto.ClothesName,
                Description = clothesDto.Description,
                CreatedDate = DateTime.Now
            };

            // Create ClothesToCategory and ClothesImage
            var clothesCategories = clothesDto.CategoryIDs.Select(catId => new ClothesToCategory { ClothesID = clothesId, CategoryID = catId }).ToList();
            var clothesImage = new ClothesImage
            {
                ImageID = Guid.NewGuid().ToString(),
                ClothesID = clothesId,
                ImagePath = imagePath,
                UploadedDate = DateTime.Now
            };

            // Save to repository
            await _clothesRepository.CreateClothesAsync(clothes, clothesCategories, clothesImage);

            return new ClothesDTO
            {
                ClothesID = clothes.ClothesID,
                ClothesName = clothes.ClothesName,
                Description = clothes.Description,
                CreatedDate = clothes.CreatedDate,
                CategoryIDs = clothesDto.CategoryIDs,
                ImagePaths = new List<string> { imagePath }
            };
        }

        public async Task<List<ClothesDTO>> GetAllClothesByUserAsync(string userId)
        {
            var clothesList = await _clothesRepository.GetClothesByUserAsync(userId);
            // Map to DTO and return
            return clothesList.Select(c => new ClothesDTO
            {
                ClothesID = c.ClothesID,
                ClothesName = c.ClothesName,
                Description = c.Description,
                CreatedDate = c.CreatedDate,
                CategoryIDs = c.ClothesToCategories.Select(cat => cat.CategoryID).ToList(),
                ImagePaths = c.ClothesImages.Select(img => img.ImagePath).ToList()
            }).ToList();
        }

        public async Task<ClothesDTO> GetClothesByIdAsync(string clothesId)
        {
            var clothes = await _clothesRepository.GetClothesByIdAsync(clothesId);
            if (clothes == null) return null;

            return new ClothesDTO
            {
                ClothesID = clothes.ClothesID,
                ClothesName = clothes.ClothesName,
                Description = clothes.Description,
                CreatedDate = clothes.CreatedDate,
                CategoryIDs = clothes.ClothesToCategories.Select(cat => cat.CategoryID).ToList(),
                ImagePaths = clothes.ClothesImages.Select(img => img.ImagePath).ToList()
            };
        }
    }

}
