using Application.DTOs.PaymentMethodDTO;
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
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly AppDbContext _context;

        public PaymentMethodRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentMethodDTO>> GetAllAsync()
        {
            return await _context.PaymentMethods
                .Select(pm => new PaymentMethodDTO
                {
                    PaymentMethodID = pm.PaymentMethodID,
                    PaymentMethodName = pm.MethodName,
                    Description = pm.Description
                })
                .ToListAsync();
        }

        public async Task<PaymentMethodDTO> GetByIdAsync(string paymentMethodID)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(paymentMethodID);
            if (paymentMethod == null) return null;

            return new PaymentMethodDTO
            {
                PaymentMethodID = paymentMethod.PaymentMethodID,
                PaymentMethodName = paymentMethod.MethodName,
                Description = paymentMethod.Description
            };
        }

        public async Task<PaymentMethodDTO> CreateAsync(PaymentMethodCreateUpdateDTO paymentMethodDTO)
        {
            // Tạo đối tượng PaymentMethod mới mà không gán ID từ DTO
            var paymentMethod = new PaymentMethod
            {
                MethodName = paymentMethodDTO.PaymentMethodName,
                Description = paymentMethodDTO.Description
            };

            // Thêm vào context và lưu
            _context.PaymentMethods.Add(paymentMethod);
            await _context.SaveChangesAsync();

            // Trả về DTO đã được gán ID mới
            return new PaymentMethodDTO
            {
                PaymentMethodID = paymentMethod.PaymentMethodID, // ID đã được sinh ra tự động
                PaymentMethodName = paymentMethod.MethodName,
                Description = paymentMethod.Description
            };
        }

        public async Task<bool> UpdateAsync(string paymentMethodID, PaymentMethodCreateUpdateDTO paymentMethodDTO)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(paymentMethodID);
            if (paymentMethod == null) return false;

            paymentMethod.MethodName = paymentMethodDTO.PaymentMethodName;
            paymentMethod.Description = paymentMethodDTO.Description;

            _context.PaymentMethods.Update(paymentMethod);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string paymentMethodID)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(paymentMethodID);
            if (paymentMethod == null) return false;

            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string> GenerateNewPaymentMethodIdAsync()
        {
            // Generate a new PaymentMethodID according to your needs.
            // Example: ROLE_1, ROLE_2, etc.
            var count = await _context.PaymentMethods.CountAsync();
            return $"METHOD_{count + 1}";
        }
    }
}
