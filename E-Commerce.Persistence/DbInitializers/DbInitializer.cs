using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace E_Commerce.Persistence.DbInitializers
{
    public class DbInitializer(StoreDbContext context) : IDbInitializer
    {
        public void Initialize()
        {
            context.Database.Migrate();
            if(!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText(@"..\E-Commerce.Persistence\Context\DataSeed/brands.json");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData, options);
                if (brands != null && brands.Any())
                {
                    context.ProductBrands.AddRange(brands);
                    context.SaveChanges();
                }
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText(@"..\E-Commerce.Persistence\Context\DataSeed/types.json");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData, options);
                if (types != null && types.Any())
                {
                    context.ProductTypes.AddRange(types);
                    context.SaveChanges();
                }
            }
            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText(@"..\E-Commerce.Persistence\Context\DataSeed/products.json");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var products = JsonSerializer.Deserialize<List<Product>>(productsData, options);
                if (products != null && products.Any())
                {
                    context.Products.AddRange(products);
                    context.SaveChanges();
                }
            }
        }
    }
}
