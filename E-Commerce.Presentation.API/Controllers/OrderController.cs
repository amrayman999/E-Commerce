using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.Dtos.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace E_Commerce.Presentation.API.Controllers
{
    public class OrderController(IServiceManager _serviceManager) : APIBaseController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderRequest request)
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.OrderService.CreateOrderAsync(request, userEmailClaim.Value);
            return Ok(result);
        }
    }
}
