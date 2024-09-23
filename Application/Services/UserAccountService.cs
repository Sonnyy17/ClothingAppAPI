using Application.DTOs.AccountDTO;
using Application.Interfaces;
using Application.Repositories;
using Infrastructure.Security;
using Org.BouncyCastle.Crypto.Generators;
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
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public UserAccountService(IUserAccountRepository repository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _repository = repository;
            _jwtTokenGenerator = jwtTokenGenerator;
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

        public async Task<string> LoginAsync(LoginAccountDTO loginDto)
        {
            var userAccount = await _repository.GetByUsernameAsync(loginDto.Username);

            //if (userAccount == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, userAccount.Password))
            if (userAccount == null || loginDto.Password != userAccount.Password)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            return _jwtTokenGenerator.GenerateToken(userAccount.UserID, userAccount.Username, userAccount.RoleID);
        }

        public async Task<ViewAccountDTO> UpdateAsync(string userId, UpdateAccountDTO updateDTO)
        {
            return await _repository.UpdateAsync(userId, updateDTO);
        }
        public async Task<ViewAccountDTO> UpdateRoleAccountAsync(string userId, UpdateRoleAccountDTO updateRoleDTO)
        {
            return await _repository.UpdateRoleAccountAsync(userId, updateRoleDTO);
        }

        public async Task DeleteByIdAsync(string userId)
        {
            await _repository.DeleteByIdAsync(userId);
        }


        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            // Hàm xác thực mật khẩu (hash hoặc sử dụng các kỹ thuật bảo mật)
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword);
        }
    }
}
