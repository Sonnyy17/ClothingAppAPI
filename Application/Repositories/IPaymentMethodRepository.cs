using Application.DTOs.PaymentMethodDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task<IEnumerable<PaymentMethodDTO>> GetAllAsync();
        Task<PaymentMethodDTO> GetByIdAsync(string paymentMethodID);
        Task<PaymentMethodDTO> CreateAsync(PaymentMethodCreateUpdateDTO paymentMethodDTO);
        Task<bool> UpdateAsync(string paymentMethodID, PaymentMethodCreateUpdateDTO paymentMethodDTO);
        Task<bool> DeleteAsync(string paymentMethodID);
        Task<string> GenerateNewPaymentMethodIdAsync();
    }
}
