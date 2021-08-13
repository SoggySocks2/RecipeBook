using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RecipeBook.CoreApp.Infrastructure.Data.Account.Seeds;
using RecipeBook.SharedKernel.Contracts;
using System;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Infrastructure.Data
{
    public class CoreDbInitializer
    {
        private readonly IConfiguration _configuration;
        private readonly CoreDbContext _dbContext;
        private readonly ILogWriter _logWriter;

        public CoreDbInitializer(IConfiguration configuration, CoreDbContext dbContext, ILogWriter logWriter)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _logWriter = logWriter;
        }

        /// <summary>
        /// Seed the database
        /// </summary>
        /// <param name="retry">Number of attempts to seed the database.</param>
        public async Task Seed(int retry = 0)
        {
            try
            {
                var salt = _configuration.GetValue<string>("Salt");
                await SeedUserAccount(salt);
            }
            catch (Exception ex)
            {
                _logWriter.LogError("Error Occurred while seeding user accounts: " + ex.Message);

                if (retry > 0)
                {
                    await Seed(retry - 1);
                }
            }
        }

        private async Task SeedUserAccount(string salt)
        {
            if (! await _dbContext.UserAccounts.AnyAsync())
            {
                var hashedPassword = Account.UserAccountRepository.HashPassword("Password", salt);
                _dbContext.UserAccounts.AddRange(UserAccountSeed.GetUserAccounts("Firstname_", "Lastname_", "Username_", hashedPassword, "Admin"));
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
