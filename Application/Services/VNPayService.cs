using Application.DTOs.VNPayDTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VNPayService : IVNPayService
    {
        private readonly IConfiguration _config;

        public VNPayService(IConfiguration config)
        {
            config = _config;
        }

        public Task<string> CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<VnPaymentResponseModel> PaymentExcute(IQueryCollection collections)
        {
            throw new NotImplementedException();
        }
    }
}
