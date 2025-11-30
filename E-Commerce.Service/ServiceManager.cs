using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Abstraction.Baskets;
using E_Commerce.Service.Abstraction.Cache;
using E_Commerce.Service.Abstraction.Products;
using E_Commerce.Service.Baskets;
using E_Commerce.Service.Cache;
using E_Commerce.Service.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service
{
    public class ServiceManager(
        IUnitOfWork _unitOfWork,
        IMapper _mapper ,
        IBasketRepository _basketRepository,
        ICacheRepository _cacheRepository) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);
        public IBasketService BasketService { get; } = new BasketService(_basketRepository, _mapper);
        public ICacheService CacheService { get; } = new CacheService(_cacheRepository);

    }
}
