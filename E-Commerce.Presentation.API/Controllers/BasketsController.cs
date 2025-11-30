using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Abstraction.Baskets;
using E_Commerce.Shared.Dtos.Baskets;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.API.Controllers
{
    public class BasketsController(IServiceManager _serviceManager) : APIBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
            var result = await _serviceManager.BasketService.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasket(BasketDto dto)
        {
            var result = await _serviceManager.BasketService.CreateBasketAsync(dto, TimeSpan.FromDays(1));
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketById(string id)
        {
            var result = await _serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
