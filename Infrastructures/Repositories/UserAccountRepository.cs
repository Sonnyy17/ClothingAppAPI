using Application.DTOs.AccountDTO;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly AppDbContext _context;

        public UserAccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ViewAccountDTO> GetByIdAsync(string userId)
        {
            var user = await _context.UserAccounts.FindAsync(userId);
            if (user == null) return null;

            return new ViewAccountDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Password = user.Password,
                CreatedDate = user.CreatedDate,
                LastLoginDate = user.LastLoginDate,
                RoleID = user.RoleID
            };
        }
        public async Task<ViewAccountDTO> GetByUsernameAsync(string username)
        {
            var user = await _context.UserAccounts.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            return new ViewAccountDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Password = user.Password,
                CreatedDate = user.CreatedDate,
                LastLoginDate = user.LastLoginDate,
                RoleID = user.RoleID
            };
        }

            public async Task<IEnumerable<ViewAccountDTO>> GetAllAsync()
        {
            return await _context.UserAccounts.Select(user => new ViewAccountDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Password = user.Password,
                CreatedDate = user.CreatedDate,
                LastLoginDate = user.LastLoginDate,
                RoleID = user.RoleID
            }).ToListAsync();
        }

        public async Task<ViewAccountDTO> RegisterAsync(RegisterAccountDTO registerDTO)
        {
            // Kiểm tra Username đã tồn tại
            if (await _context.UserAccounts.AnyAsync(u => u.Username == registerDTO.Username))
            {
                throw new Exception("Username đã tồn tại.");
            }

            // Kiểm tra Password và ConfirmPassword
            if (registerDTO.Password != registerDTO.ConfirmPasword)
            {
                throw new Exception("Password và ConfirmPassword không khớp.");
            }

            var newUserId = await GenerateNewUserIdAsync();
            var currentDate = DateTime.Now;

            var newUser = new UserAccount
            {
                UserID = newUserId,
                Username = registerDTO.Username,
                Password = registerDTO.Password,
                CreatedDate = currentDate,
                LastLoginDate = currentDate,
                RoleID = "ROLE_1"
            };

            _context.UserAccounts.Add(newUser);
            await _context.SaveChangesAsync();

            return new ViewAccountDTO
            {
                UserID = newUser.UserID,
                Username = newUser.Username,
                Password = newUser.Password,
                CreatedDate = newUser.CreatedDate,
                LastLoginDate = newUser.LastLoginDate,
                RoleID = newUser.RoleID
            };
        }

        public async Task<ViewAccountDTO> LoginAsync(LoginAccountDTO loginDTO)
        {
            var user = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.Username == loginDTO.Username && u.Password == loginDTO.Password);

            if (user == null)
            {
                throw new Exception("Đăng nhập thất bại, kiểm tra lại username hoặc password.");
            }

            // Cập nhật LastLoginDate
            user.LastLoginDate = DateTime.Now;
            _context.UserAccounts.Update(user);
            await _context.SaveChangesAsync();

            return new ViewAccountDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Password = user.Password,
                CreatedDate = user.CreatedDate,
                LastLoginDate = user.LastLoginDate,
                RoleID = user.RoleID
            };
        }
        public async Task<ViewAccountDTO> UpdateAsync(string userId, UpdateAccountDTO updateDTO)
        {
            var user = await _context.UserAccounts.FindAsync(userId);
            if (user == null) throw new Exception("User không tồn tại.");

            // Kiểm tra Username mới nếu có thay đổi
            if (user.Username != updateDTO.Username && await _context.UserAccounts.AnyAsync(u => u.Username == updateDTO.Username))
            {
                throw new Exception("Username đã tồn tại.");
            }

            // Kiểm tra mật khẩu cũ
            if (user.Password != updateDTO.OldPassword)
            {
                throw new Exception("Mật khẩu cũ không chính xác.");
            }

            // Kiểm tra mật khẩu mới
            if (updateDTO.NewPassword == updateDTO.OldPassword)
            {
                throw new Exception("Mật khẩu mới phải khác mật khẩu cũ.");
            }

            if (updateDTO.NewPassword != updateDTO.ConfirmNewPassword)
            {
                throw new Exception("Mật khẩu mới và mật khẩu xác nhận không khớp.");
            }

            // Cập nhật thông tin tài khoản
            user.Username = updateDTO.Username;
            user.Password = updateDTO.NewPassword;

            _context.UserAccounts.Update(user);
            await _context.SaveChangesAsync();

            return new ViewAccountDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Password = user.Password,
                CreatedDate = user.CreatedDate,
                LastLoginDate = user.LastLoginDate,
                RoleID = user.RoleID
            };
        }

        public async Task<bool> DeleteByIdAsync(string userId)
        {
            var user = await _context.UserAccounts.FindAsync(userId);
            if (user == null) return false;

            _context.UserAccounts.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string> GenerateNewUserIdAsync()
        {
            var maxId = await _context.UserAccounts
                .OrderByDescending(u => u.UserID)
                .Select(u => u.UserID)
                .FirstOrDefaultAsync();

            int nextId = 1;
            if (!string.IsNullOrEmpty(maxId))
            {
                nextId = int.Parse(maxId.Split('_').Last()) + 1;
            }

            return $"USER_{nextId.ToString("D2")}";
        }
    }
}
