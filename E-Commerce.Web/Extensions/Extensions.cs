using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Persistence;
using E_Commerce.Persistence.Identity.Contexts;
using E_Commerce.Service;
using E_Commerce.Shared.ErrorModels;
using E_Commerce.Web.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace E_Commerce.Web.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWebServices();
            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices(configuration);
            services.ConfigureApiBehaviorOptions();
            services.AddIdentityServices();
            return services;
        }
        private static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityStoreDbContext>();
            return services;
        }
        private static IServiceCollection ConfigureApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Any())
                                                         .Select(M => new ValidationError()
                                                         {
                                                             Field = M.Key,
                                                             Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                                                         }).ToList();
                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }

        public static async Task<WebApplication> ConfigureMiddlewares(this WebApplication app)
        {
            #region Initialize Db
            await app.SeedData();
            #endregion
            app.UseGlobalErrorHandling();
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            app.UseStaticFiles();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            return app;
        }
        private static async Task<WebApplication> SeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await initializer.InitializeAsync();
            await initializer.InitializeIdentityAsync();
            return app;
        }
        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}
