using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RecipeBook.ApiGateway.Api.Features.Identity;
using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;
using RecipeBook.CoreApp.Infrastructure.Data;
using RecipeBook.CoreApp.Infrastructure.Data.UserAccounts;
using RecipeBook.CoreAppTests.Shared.UserAccounts.Builders;
using RecipeBook.SharedKernel.CustomExceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecipeBook.CoreAppTests.Infrastructure.IntegrationTests.Auth
{
    public class AuthRepositoryTests
    {
        private readonly CoreDbContext _dbContext;
        private readonly IUserAccountRepository _authRepo;

        public AuthRepositoryTests(IConfiguration configuration)
        {
            var dbOptions = new DbContextOptionsBuilder<CoreDbContext>()
                //.UseInMemoryDatabase(databaseName: "RecipeBook")
                .UseSqlServer("Data Source=HOME-DEV-PC\\SQL2016;Initial Catalog=RecipeBook;Integrated Security=SSPI;ConnectRetryCount=0;")
                .Options;

            var authenticatedUser = new AuthenticatedUser();
            _dbContext = new CoreDbContext(dbOptions, authenticatedUser);
            _authRepo = new UserAccountRepository(configuration, _dbContext);
        }

        [Fact]
        public void AuthenticateAsync_WhenCredentialsNotValid_ThrowsAuthenticationException()
        {
            Func<Task> dataProvider = async () =>
            {
                _ = await _authRepo.AuthenticateAsync("wrong", "password", CancellationToken.None);
            };

            dataProvider.Should().Throw<AuthenticateException>()
                .WithMessage("Authentication failed");
        }

        [Fact]
        public async void AuthenticateAsync_WhenValid_Authenticates()
        {
            var testUserAccount = new UserAccountBuilder()
                                    .WithTestValues()
                                    .Build();

            var id = await _authRepo.AddAsync(testUserAccount, CancellationToken.None);

            id.Should().NotBe(Guid.Empty);

            var userAccount = await _authRepo.AuthenticateAsync(testUserAccount.Username, testUserAccount.Password, CancellationToken.None);

            userAccount.Id.Should().NotBe(Guid.Empty);
            userAccount.Person.FirstName.Should().Be(testUserAccount.Person.FirstName);
            userAccount.Person.LastName.Should().Be(testUserAccount.Person.LastName);
        }
    }
}
