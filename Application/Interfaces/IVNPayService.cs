using Application.DTOs.VNPayDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVNPayService
    {
        Task<string> CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        Task<VnPaymentResponseModel> PaymentExcute(IQueryCollection collections);
    }
}
