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
            // Tìm quần áo theo ClothesID
            var clothes = await _clothesRepository.SearchOneAsync(clothesId);
            if (clothes == null)
            {
                throw new Exception("Clothes not found");
            }

            // Giữ nguyên giá trị cũ nếu các trường không có giá trị mới
            clothes.ClothesName = !string.IsNullOrEmpty(dto.ClothesName) ? dto.ClothesName : clothes.ClothesName;
            clothes.Description = !string.IsNullOrEmpty(dto.Description) ? dto.Description : clothes.Description;

            // Kiểm tra các CategoryIDs có tồn tại trong database không
            if (dto.CategoryIDs != null && dto.CategoryIDs.Any())
            {
                var validCategoryIds = await _clothesRepository.ValidateCategoryIdsAsync(dto.CategoryIDs);
                if (validCategoryIds.Count != dto.CategoryIDs.Count)
                {
                    throw new Exception("Some category IDs are invalid.");
                }
            }

            // Cập nhật quần áo và danh mục
            await _clothesRepository.UpdateClothesAsync(clothes, dto.CategoryIDs);
        }



        public async Task DeleteClothesAsync(string clothesId)
        {
            // Kiểm tra nếu ClothesID không tồn tại và ném ngoại lệ nếu không tìm thấy
            var clothes = await _clothesRepository.SearchOneAsync(clothesId);
            if (clothes == null)
            {
                throw new Exception("Clothes not found.");
            }

            // Nếu tồn tại, tiến hành xóa
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
