using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.CoreApp.Api.Features.Services;
using RecipeBook.CoreApp.Api.Features.UserAccount.Contracts;
using RecipeBook.CoreApp.Domain.Account.Contracts;
using RecipeBook.CoreApp.Infrastructure.Data;
using RecipeBook.CoreApp.Infrastructure.Data.Account;
using RecipeBook.CoreApp.Infrastructure.Logging;
using RecipeBook.SharedKernel.Contracts;

namespace RecipeBook.CoreApp.Api.Configuration
{
    public static class CoreAppServicesConfiguration
    {
        public static void AddCoreAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            /* Add the customer database context */
            //var connectionStringFromAzureKeyVault = configuration.GetSection(clientSettings.DatabaseSecretName).Get<string>();
            //var connectionStringFromLocalAppSettings = configuration.GetConnectionString("CoreAppDbConnection");
            //if (!string.IsNullOrEmpty(connectionStringFromLocalAppSettings))
            //{
            //    services.AddDbContext<CoreDbContext>(options => options.UseSqlServer(connectionStringFromLocalAppSettings));
            //}
            //else if (!string.IsNullOrEmpty(connectionStringFromAzureKeyVault))
            //{
            //    services.AddDbContext<CoreDbContext>(options => options.UseSqlServer(connectionStringFromAzureKeyVault));
            //}
            //else
            //{
            //    throw new NotFoundException("No Connection String provided");
            //}

            //var connectionStringFromLocalAppSettings = configuration.GetSection("ConnectionStrings").Get<string>();
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

            /* Authentication service to interface with the customer repository */
            services.AddScoped<IUserAccountService, UserAccountService>();
        }
    }
}
