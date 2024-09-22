using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IClothesRepository
    {
        Task<int> GetUserClothesCountAsync(string userId);

        Task CreateClothesAsync(Clothes clothes, List<ClothesToCategory> categories, ClothesImage image);

        Task<List<Clothes>> GetClothesByUserAsync(string userId);

        Task<Clothes> GetClothesByIdAsync(string clothesId);
    }

}
