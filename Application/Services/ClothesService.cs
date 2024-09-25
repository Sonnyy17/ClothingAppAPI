using Application.DTOs.ClothesDTO;
using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Logging;
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
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<ClothesService> _logger;

        public ClothesService(IClothesRepository clothesRepository, ICloudinaryService cloudinaryService, ILogger<ClothesService> logger)
        {
            _clothesRepository = clothesRepository;
            _cloudinaryService = cloudinaryService;
            _logger = logger; // Khởi tạo logger

        }
        /*
        public async Task CreateClothesAsync(string userId, CreateClothesDTO dto)
        {
            var clothesId = await _clothesRepository.GenerateClothesIDAsync(userId);
            var clothes = new Clothes
            {
                ClothesID = clothesId,
                UserID = userId,
                ClothesName = dto.ClothesName,
                Description = dto.Description,
                CreatedDate = DateTime.Now
            };

            await _clothesRepository.CreateClothesAsync(clothes);

            if (dto.Image != null)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(dto.Image);
                await _clothesRepository.AddImageAsync(clothesId, imageUrl);
            }

            if (dto.CategoryIDs != null && dto.CategoryIDs.Any())
            {
                await _clothesRepository.AddCategoriesAsync(clothesId, dto.CategoryIDs);
            }
        }
        */

        public async Task CreateClothesAsync(string userId, CreateClothesDTO dto)
        {
            try
            {
                _logger.LogInformation("Creating clothes for user: {UserId}", userId);

                var clothesId = await _clothesRepository.GenerateClothesIDAsync(userId);
                _logger.LogInformation("Generated Clothes ID: {ClothesID}", clothesId);

                // Lưu thông tin Clothes vào database
                var clothes = new Clothes
                {
                    ClothesID = clothesId,
                    UserID = userId,
                    ClothesName = dto.ClothesName,
                    Description = dto.Description,
                    CreatedDate = DateTime.Now
                };

                await _clothesRepository.CreateClothesAsync(clothes);

                // Lưu ảnh lên Cloudinary và tạo ClothesImage
                var imagePath = await _cloudinaryService.UploadImageAsync(dto.Image);
                _logger.LogInformation("Image uploaded to Cloudinary: {ImagePath}", imagePath);

                await _clothesRepository.AddImageAsync(clothesId, imagePath);

                // Thêm các categories liên quan
                await _clothesRepository.AddCategoriesAsync(clothesId, dto.CategoryIDs);

                _logger.LogInformation("Clothes created successfully for user: {UserId}", userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating clothes for user: {UserId}", userId);
                throw;
            }
        }


        public async Task UpdateClothesAsync(string clothesId, UpdateClothesDTO dto)
        {
            var clothes = await _clothesRepository.SearchOneAsync(clothesId);
            if (clothes == null)
            {
                throw new Exception("Clothes not found");
            }

            clothes.ClothesName = dto.ClothesName;
            clothes.Description = dto.Description;

            await _clothesRepository.UpdateClothesAsync(clothes);

            // Update categories
            await _clothesRepository.DeleteCategoriesAsync(clothesId);
            if (dto.CategoryIDs != null && dto.CategoryIDs.Any())
            {
                await _clothesRepository.AddCategoriesAsync(clothesId, dto.CategoryIDs);
            }
        }

        public async Task DeleteClothesAsync(string clothesId)
        {
            await _clothesRepository.DeleteClothesAsync(clothesId);
        }

        public async Task<List<ClothesResponseDTO>> SearchMultipleAsync(string userId, List<string> categoryIds)
        {
            var clothesList = await _clothesRepository.SearchMultipleAsync(userId, categoryIds);
            var responseList = new List<ClothesResponseDTO>();

            foreach (var clothes in clothesList)
            {
                var categoryIdsForClothes = await _clothesRepository.GetCategoryIdsForClothesAsync(clothes.ClothesID);
                responseList.Add(new ClothesResponseDTO
                {
                    ClothesID = clothes.ClothesID,
                    ClothesName = clothes.ClothesName,
                    Description = clothes.Description,
                    ImagePath = await _clothesRepository.GetImagePathAsync(clothes.ClothesID),
                    CreatedDate = clothes.CreatedDate,
                    CategoryIDs = categoryIdsForClothes
                });
            }

            return responseList;
        }

        public async Task<ClothesResponseDTO> SearchOneAsync(string clothesId)
        {
            var clothes = await _clothesRepository.SearchOneAsync(clothesId);
            if (clothes == null)
            {
                return null;
            }

            var categoryIds = await _clothesRepository.GetCategoryIdsForClothesAsync(clothesId);

            return new ClothesResponseDTO
            {
                ClothesID = clothes.ClothesID,
                ClothesName = clothes.ClothesName,
                Description = clothes.Description,
                ImagePath = await _clothesRepository.GetImagePathAsync(clothes.ClothesID),
                CreatedDate = clothes.CreatedDate,
                CategoryIDs = categoryIds
            };
        }
    }

}
