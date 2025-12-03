using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Persistence.Data;
using E_Commerce.Persistence.Identity.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace E_Commerce.Persistence.DbInitializers
{
    public class DbInitializer(StoreDbContext context,
        IdentityStoreDbContext _identityContext,
        UserManager<AppUser> _userManager,
        RoleManager<IdentityRole> _roleManager) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            await context.Database.MigrateAsync();
            if(!context.ProductBrands.Any()) 
            {
                var brandsData = await File.ReadAllTextAsync(@"..\E-Commerce.Persistence\Data\DataSeed\brands.json");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData, options);
                if (brands != null && brands.Any())
                {
                    context.ProductBrands.AddRange(brands);
                }
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = await File.ReadAllTextAsync(@"..\E-Commerce.Persistence\Data\DataSeed\types.json");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData, options);
                if (types != null && types.Any())
                {
                    context.ProductTypes.AddRange(types);
                }
            }
            if (!context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync(@"..\E-Commerce.Persistence\Data\DataSeed\products.json");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var products = JsonSerializer.Deserialize<List<Product>>(productsData, options);
                if (products != null && products.Any())
                {
                    context.Products.AddRange(products);
                }
            }
            if (!context.DeliveryMethods.Any())
            {
                var deliveryData = await File.ReadAllTextAsync(@"..\E-Commerce.Persistence\Data\DataSeed\delivery.json");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData, options);
                if (deliveryMethods != null && deliveryMethods.Any())
                {
                    context.DeliveryMethods.AddRange(deliveryMethods);
                }
            }
            await context.SaveChangesAsync();

        }
        public async Task InitializeIdentityAsync()
        {

            await _identityContext.Database.MigrateAsync();
            if (!_identityContext.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "SuperAdmin" });
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            }

            if (!_identityContext.Users.Any())
            {
                var superAdmin = new AppUser
                {
                    UserName = "SuperAdmin",
                    DisplayName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    PhoneNumber = "01233345555"
                };
                var admin = new AppUser
                {
                    UserName = "Admin",
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "01233345555"
                };
                await _userManager.CreateAsync(superAdmin, "P@ssw0rd");
                await _userManager.CreateAsync(admin, "P@ssw0rd");

                await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                await _userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
