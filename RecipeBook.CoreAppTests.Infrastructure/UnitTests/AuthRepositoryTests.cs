using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RecipeBook.ApiGateway.Api.Features.Identity;
using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;
using RecipeBook.CoreApp.Infrastructure.Data;
using RecipeBook.CoreApp.Infrastructure.Data.UserAccounts;
using RecipeBook.SharedKernel.CustomExceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecipeBook.CoreAppTests.Infrastructure.UnitTests
{
    public class AuthRepositoryTests
    {
        private readonly CoreDbContext _dbContext;
        private readonly IUserAccountRepository _authRepo;

        private readonly string _userName = $"Test Username {Guid.NewGuid()}";
        private readonly string _password = Guid.NewGuid().ToString();

        public AuthRepositoryTests(IConfiguration configuration)
        {
            var dbOptions = new DbContextOptionsBuilder<CoreDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipeBook")
                .Options;

            var authenticatedUser = new AuthenticatedUser(); ;
            _dbContext = new CoreDbContext(dbOptions, authenticatedUser);
            _authRepo = new UserAccountRepository(configuration, _dbContext);
        }

        [Fact]
        public void AuthenticateAsync_WhenUserNameIsMissing_ThrowsEmptyInputException()
        {
            Func<Task> userAccount = async () =>
            {
                _ = await _authRepo.AuthenticateAsync(string.Empty, _password, CancellationToken.None);
            };

            userAccount.Should().Throw<EmptyInputException>()
                .WithMessage("userName is required");
        }

        [Fact]
        public void AuthenticateAsync_WhenPasswordIsMissing_ThrowsEmptyInputException()
        {
            Func<Task> userAccount = async () =>
            {
                _ = await _authRepo.AuthenticateAsync(_userName, string.Empty, CancellationToken.None);
            };

            userAccount.Should().Throw<EmptyInputException>()
                .WithMessage("password is required");
        }
    }
}
