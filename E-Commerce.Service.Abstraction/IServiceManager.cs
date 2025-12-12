using E_Commerce.Service.Abstraction.Auth;
using E_Commerce.Service.Abstraction.Baskets;
using E_Commerce.Service.Abstraction.Cache;
using E_Commerce.Service.Abstraction.Orders;
using E_Commerce.Service.Abstraction.Payments;
using E_Commerce.Service.Abstraction.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Abstraction
{
    public interface IServiceManager
    {
        IProductService ProductService {  get; }
        IBasketService BasketService {  get; }
        ICacheService CacheService { get; }
        IAuthService AuthService { get; }
        IOrderService OrderService { get; }
        IPaymentService PaymentService { get; }

    }
}
