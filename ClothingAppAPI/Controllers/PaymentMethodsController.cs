using Application.DTOs.PaymentMethodDTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodsController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var paymentMethods = await _paymentMethodService.GetAllPaymentMethodsAsync();
            return Ok(paymentMethods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var paymentMethod = await _paymentMethodService.GetPaymentMethodByIdAsync(id);
            if (paymentMethod == null) return NotFound("PaymentMethodID không tồn tại.");
            return Ok(paymentMethod);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentMethodCreateUpdateDTO dto)
        {
            var paymentMethod = await _paymentMethodService.CreatePaymentMethodAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = paymentMethod.PaymentMethodID }, paymentMethod);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] PaymentMethodCreateUpdateDTO dto)
        {
            var result = await _paymentMethodService.UpdatePaymentMethodAsync(id, dto);
            if (!result) return NotFound("PaymentMethodID không tồn tại.");

            // Lấy lại phương thức thanh toán đã cập nhật
            var updatedPaymentMethod = await _paymentMethodService.GetPaymentMethodByIdAsync(id);
            return Ok(new
            {
                Message = "Update successfully",
                PaymentMethod = updatedPaymentMethod
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _paymentMethodService.DeletePaymentMethodAsync(id);
            if (!result) return NotFound("PaymentMethodID không tồn tại.");

            return Ok(new { Message = "Delete successfully" });
        }
    }

}
