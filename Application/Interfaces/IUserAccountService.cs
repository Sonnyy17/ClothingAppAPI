using Application.DTOs.AccountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserAccountService
    {
        Task<ViewAccountDTO> GetByIdAsync(string userId);
        Task<ViewAccountDTO> GetByUsernameAsync(string username);
        Task<IEnumerable<ViewAccountDTO>> GetAllAsync();
        Task<ViewAccountDTO> RegisterAsync(RegisterAccountDTO registerDTO);
        Task<string> LoginAsync(LoginAccountDTO loginDto);
        Task<ViewAccountDTO> UpdateAsync(string userId, UpdateAccountDTO updateDTO);
        Task<ViewAccountDTO> UpdateRoleAccountAsync(string userId, UpdateRoleAccountDTO updateRoleDTO);
        Task DeleteByIdAsync(string userId);
    }
}
