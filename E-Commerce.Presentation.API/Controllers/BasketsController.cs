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
    public class BasketsController(IBasketService _basketService) : APIBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
            var result = await _basketService.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasket(BasketDto dto)
        {
            var result = await _basketService.CreateBasketAsync(dto, TimeSpan.FromDays(1));
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketById(string id)
        {
            var result = await _basketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
