using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecipeBook.CoreApp.Infrastructure.Data;
using System.Threading.Tasks;

namespace RecipeBook.ApiGateway.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var environment = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();
                if (environment.IsDevelopment())
                {
                    var dbInitializer = scope.ServiceProvider.GetRequiredService<CoreDbInitializer>();
                    await dbInitializer.Seed();
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
