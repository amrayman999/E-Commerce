using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Abstraction.Auth;
using E_Commerce.Service.Abstraction.Baskets;
using E_Commerce.Service.Abstraction.Cache;
using E_Commerce.Service.Abstraction.Products;
using E_Commerce.Service.Auth;
using E_Commerce.Service.Baskets;
using E_Commerce.Service.Cache;
using E_Commerce.Service.Products;
using E_Commerce.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;


namespace E_Commerce.Service
{
    public class ServiceManager(
        IUnitOfWork _unitOfWork,
        IMapper _mapper ,
        IBasketRepository _basketRepository,
        ICacheRepository _cacheRepository,
        UserManager<AppUser> _userManager, 
        IOptions<JwtOptions> options ) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);
        public IBasketService BasketService { get; } = new BasketService(_basketRepository, _mapper);
        public ICacheService CacheService { get; } = new CacheService(_cacheRepository);
        public IAuthService AuthService { get; } = new AuthService(_userManager, options);

    }
}
