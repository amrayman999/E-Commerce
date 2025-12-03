using E_Commerce.Web.Extensions;


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
