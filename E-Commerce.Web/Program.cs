using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence;
using E_Commerce.Persistence.Context;
using E_Commerce.Persistence.DbInitializers;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.ErrorModels;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAllServices(builder.Configuration);
            var app = builder.Build();
            await app.ConfigureMiddlewares();
            app.Run();
        }
    }
}
