using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RecipeBook.CoreApp.Domain.Account.Contracts;
using RecipeBook.CoreApp.Infrastructure.Data;
using RecipeBook.CoreApp.Infrastructure.Data.Account;
using RecipeBook.CoreAppTests.Shared.UserAccount.Builders;
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
        private readonly IAuthRepository _authRepo;
        private const string _salt = "MDxNyrhRlHee7I0CTW9fzVk=";

        public AuthRepositoryTests()
        {
            var dbOptions = new DbContextOptionsBuilder<CoreDbContext>()
                //.UseInMemoryDatabase(databaseName: "RecipeBook")
                .UseSqlServer("Data Source=HOME-DEV-PC\\SQL2016;Initial Catalog=RecipeBook;Integrated Security=SSPI;ConnectRetryCount=0;")
                .Options;

            _dbContext = new CoreDbContext(dbOptions);
            _authRepo = new AuthRepository(_dbContext);
        }

        [Fact]
        public void AuthenticateAsync_WhenCredentialsNotValid_ThrowsAuthenticationException()
        {
            Func<Task> dataProvider = async () =>
            {
                _ = await _authRepo.AuthenticateAsync("wrong", "password", _salt, CancellationToken.None);
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

            var id = await _authRepo.AddAsync(testUserAccount.Firstname, testUserAccount.Lastname, testUserAccount.Username, testUserAccount.Password, _salt, CancellationToken.None);

            id.Should().NotBe(Guid.Empty);

            var userAccount = await _authRepo.AuthenticateAsync(testUserAccount.Username, testUserAccount.Password, _salt, CancellationToken.None);

            userAccount.Id.Should().NotBe(Guid.Empty);
            userAccount.Firstname.Should().Be(testUserAccount.Firstname);
            userAccount.Lastname.Should().Be(testUserAccount.Lastname);
        }
    }
}
