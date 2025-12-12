using E_Commerce.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.API.Controllers
{
    public class PaymentController(IServiceManager _serviceManager) : APIBaseController
    {
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
            var result = await _serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(result);
        }

        // stripe listen --forward-to https://localhost:7245/api/payment/webhook
        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            await _serviceManager.PaymentService.UpdateOrderPaymentStatusAsync(json,
                Request.Headers["Stripe-Signature"]!);
            return new EmptyResult();
        }
    }
}
