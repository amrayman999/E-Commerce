using E_Commerce.Service.Abstraction;
using E_Commerce.Service.MappingProfile;
using E_Commerce.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Persistence
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddAutoMapper(x => x.AddProfile(new ProductProfile(configuration)));
            return services;
        }
    }
}
