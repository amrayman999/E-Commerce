using E_Commerce.Service.Abstraction.Baskets;
using E_Commerce.Service.Abstraction.Products;
using E_Commerce.Service.Baskets;
using E_Commerce.Service.MappingProfiles.Baskets;
using E_Commerce.Service.MappingProfiles.Products;
using E_Commerce.Service.Products;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Persistence
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(x => x.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(x => x.AddProfile(new BasketProfile()));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            return services;
        }
    }
}
