using Application.DTOs.PaymentMethodDTO;
using Application.Interfaces;
using Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PaymentMethodService  : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<IEnumerable<PaymentMethodDTO>> GetAllPaymentMethodsAsync()
        {
            return await _paymentMethodRepository.GetAllAsync();
        }

        public async Task<PaymentMethodDTO> GetPaymentMethodByIdAsync(string paymentMethodID)
        {
            return await _paymentMethodRepository.GetByIdAsync(paymentMethodID);
        }

        public async Task<PaymentMethodDTO> CreatePaymentMethodAsync(PaymentMethodCreateUpdateDTO paymentMethodDTO)
        {
            return await _paymentMethodRepository.CreateAsync(paymentMethodDTO);
        }

        public async Task<bool> UpdatePaymentMethodAsync(string paymentMethodID, PaymentMethodCreateUpdateDTO paymentMethodDTO)
        {
            return await _paymentMethodRepository.UpdateAsync(paymentMethodID, paymentMethodDTO);
        }

        public async Task<bool> DeletePaymentMethodAsync(string paymentMethodID)
        {
            return await _paymentMethodRepository.DeleteAsync(paymentMethodID);
        }
    }
}
