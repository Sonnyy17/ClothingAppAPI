using Application.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
namespace Application.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,   // Sử dụng CloudinarySettings từ appsettings.json
                config.Value.ApiKey,      // Sử dụng CloudinarySettings từ appsettings.json
                config.Value.ApiSecret    // Sử dụng CloudinarySettings từ appsettings.json
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.Url.ToString();
            }

            return null;
        }
        public async Task<bool> DeleteImageAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deletionParams);

            // Kiểm tra kết quả trả về để xác định xóa có thành công không
            return result.Result == "ok";
        }
    }
}
