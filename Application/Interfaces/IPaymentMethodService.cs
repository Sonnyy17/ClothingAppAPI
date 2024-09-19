using Application.DTOs.PaymentMethodDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethodDTO>> GetAllPaymentMethodsAsync();
        Task<PaymentMethodDTO> GetPaymentMethodByIdAsync(string paymentMethodID);
        Task<PaymentMethodDTO> CreatePaymentMethodAsync(PaymentMethodCreateUpdateDTO paymentMethodDTO);
        Task<bool> UpdatePaymentMethodAsync(string paymentMethodID, PaymentMethodCreateUpdateDTO paymentMethodDTO);
        Task<bool> DeletePaymentMethodAsync(string paymentMethodID);
    }
}
