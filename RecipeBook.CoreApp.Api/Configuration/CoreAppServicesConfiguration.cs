using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Contracts;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Services;
using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;
using RecipeBook.CoreApp.Infrastructure.Data;
using RecipeBook.CoreApp.Infrastructure.Data.UserAccounts;
using RecipeBook.CoreApp.Infrastructure.Logging;
using RecipeBook.SharedKernel.Contracts;

namespace RecipeBook.CoreApp.Api.Configuration
{
    public static class CoreAppServicesConfiguration
    {
        public static void AddCoreAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            /* Add the core app database context */
            var connectionStringFromLocalAppSettings = configuration.GetConnectionString("CoreAppDbConnection");
            services.AddDbContext<CoreDbContext>(options => options.UseSqlServer(connectionStringFromLocalAppSettings));

            /* Allow auto database migration and seeding */
            services.AddScoped<CoreDbInitializer>();

            services.AddUserAccountServices();

            services.AddScoped<ILogWriter, LogWriter>();

            /* Auto mapper used for mapping classes to DTO's, etc. */
            services.AddAutoMapper(typeof(CoreAppServicesConfiguration).Assembly);
        }

        private static void AddUserAccountServices(this IServiceCollection services)
        {
            /* Add authentication repository for all database activity */
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();

            /* Authentication service to interface with the repository */
            services.AddScoped<IUserAccountService, UserAccountService>();
        }
    }
}
