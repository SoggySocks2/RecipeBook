using Microsoft.EntityFrameworkCore;
using RecipeBook.ApiGateway.Api.Features.Identity;
using RecipeBook.CoreApp.Infrastructure.Data;
using RecipeBook.CoreAppTests.Shared.UserAccounts.Builders;

namespace RecipeBook.CoreAppTests.Shared.General
{
    public class CoreDbContextBuilder
    {
        private DbContextOptions<CoreDbContext> DbOptions { get; set; }
        private AuthenticatedUser AuthenticatedUser { get; set; }

        public CoreDbContextBuilder WithTestValues()
        {
            DbOptions = new DbContextOptionsBuilder<CoreDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipeBook")
                .Options;

            AuthenticatedUser = new AuthenticatedUserBuilder().WithTestValues().Build();

            return this;
        }
        public CoreDbContextBuilder WithTestValues(string databaseName)
        {
            DbOptions = new DbContextOptionsBuilder<CoreDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            AuthenticatedUser = new AuthenticatedUserBuilder().WithTestValues().Build();

            return this;
        }
        public CoreDbContextBuilder WithDbOptions(DbContextOptions<CoreDbContext> dbOptions)
        {
            DbOptions = dbOptions;
            return this;
        }
        public CoreDbContextBuilder WithAuthenticatedUser(AuthenticatedUser authenticatedUser)
        {
            AuthenticatedUser = authenticatedUser;
            return this;
        }

        public CoreDbContext Build()
        {
            var dbContext = new CoreDbContext(DbOptions, AuthenticatedUser);
            return dbContext;
        }
    }
}
