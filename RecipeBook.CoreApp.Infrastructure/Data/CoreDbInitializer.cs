using Microsoft.EntityFrameworkCore;
using RecipeBook.CoreApp.Infrastructure.Data.Account.Seeds;
using RecipeBook.SharedKernel.Contracts;
using System;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Infrastructure.Data
{
    public class CoreDbInitializer
    {
        private readonly CoreDbContext _dbContext;
        private readonly ILogWriter _logWriter;

        public CoreDbInitializer(CoreDbContext dbContext, ILogWriter logWriter)
        {
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
                await SeedUserAccount();
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

        private async Task SeedUserAccount()
        {
            if (await _dbContext.UserAccounts.AnyAsync())
            {
                _dbContext.UserAccounts.AddRange(UserAccountSeed.GetUserAccounts("Firstname_", "Lastname_", "Username_", "Password_"));
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
