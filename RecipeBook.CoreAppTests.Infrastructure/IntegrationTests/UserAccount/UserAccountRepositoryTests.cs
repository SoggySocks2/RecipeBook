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

namespace RecipeBook.CoreAppTests.Infrastructure.IntegrationTests.UserAccount
{
    public class UserAccountRepositoryTests
    {
        private readonly CoreDbContext _dbContext;
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountRepositoryTests()
        {
            var dbOptions = new DbContextOptionsBuilder<CoreDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipeBook")
                .Options;

            var authenticatedUser = new AuthenticatedUserBuilder().WithTestValues().Build();
            _dbContext = new CoreDbContext(dbOptions, authenticatedUser);

            var iConfiguration = new Shared.UserAccounts.Builders.IConfigurationBuilder().WithTestValues().Build();
            _userAccountRepository = new UserAccountRepository(iConfiguration, _dbContext);
        }

        [Fact]
        public void AuthenticateAsync_WhenCredentialsNotValid_ThrowsAuthenticationException()
        {
            Func<Task> userAccount = async () =>
            {
                _ = await _userAccountRepository.AuthenticateAsync("wrong", "password", CancellationToken.None);
            };

            userAccount.Should().Throw<AuthenticateException>()
                .WithMessage("Authentication failed");
        }

        [Fact]
        public async void AuthenticateAsync_WhenValid_Authenticates()
        {
            var password = "Test Password";
            var testUserAccount = new UserAccountBuilder()
                                    .WithTestValues()
                                    .WithPassword(password)
                                    .Build();

            var id = await _userAccountRepository.AddAsync(testUserAccount, CancellationToken.None);

            id.Should().NotBe(Guid.Empty);

            var userAccount = await _userAccountRepository.AuthenticateAsync(testUserAccount.Username, password, CancellationToken.None);

            userAccount.Id.Should().NotBe(Guid.Empty);
            userAccount.Person.FirstName.Should().Be(testUserAccount.Person.FirstName);
            userAccount.Person.LastName.Should().Be(testUserAccount.Person.LastName);
        }
    }
}
