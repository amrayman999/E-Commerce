using E_Commerce.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Abstraction.Baskets
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsync(string id);
        Task<BasketDto> CreateBasketAsync(BasketDto dto, TimeSpan duration);
        Task<bool> DeleteBasketAsync(string id);
    }
}
