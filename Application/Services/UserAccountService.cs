using Application.DTOs.AccountDTO;
using Application.Interfaces;
using Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _repository;

        public UserAccountService(IUserAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<ViewAccountDTO> GetByIdAsync(string userId)
        {
            return await _repository.GetByIdAsync(userId);
        }

        public async Task<ViewAccountDTO> GetByUsernameAsync(string username)
        {
            return await _repository.GetByUsernameAsync(username);
        }

        public async Task<IEnumerable<ViewAccountDTO>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ViewAccountDTO> RegisterAsync(RegisterAccountDTO registerDTO)
        {
            return await _repository.RegisterAsync(registerDTO);
        }

        public async Task<ViewAccountDTO> LoginAsync(LoginAccountDTO loginDTO)
        {
            return await _repository.LoginAsync(loginDTO);
        }
        public async Task<ViewAccountDTO> UpdateAsync(string userId, UpdateAccountDTO updateDTO)
        {
            return await _repository.UpdateAsync(userId, updateDTO);
        }

        public async Task<bool> DeleteByIdAsync(string userId)
        {
            return await _repository.DeleteByIdAsync(userId);
        }
    }
}
