using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
        private const string _salt = "MDxNyrhRlHee7I0CTW9fzVk=";

        public AuthRepositoryTests()
        {
            var dbOptions = new DbContextOptionsBuilder<CoreDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipeBook")
                .Options;

            _dbContext = new CoreDbContext(dbOptions);
            _authRepo = new UserAccountRepository(_dbContext);
        }

        [Fact]
        public void AuthenticateAsync_WhenUserNameIsMissing_ThrowsEmptyInputException()
        {
            Func<Task> userAccount = async () =>
            {
                _ = await _authRepo.AuthenticateAsync(string.Empty, _password, _salt, CancellationToken.None);
            };

            userAccount.Should().Throw<EmptyInputException>()
                .WithMessage("userName is required");
        }

        [Fact]
        public void AuthenticateAsync_WhenPasswordIsMissing_ThrowsEmptyInputException()
        {
            Func<Task> userAccount = async () =>
            {
                _ = await _authRepo.AuthenticateAsync(_userName, string.Empty, _salt, CancellationToken.None);
            };

            userAccount.Should().Throw<EmptyInputException>()
                .WithMessage("password is required");
        }

        [Fact]
        public void AuthenticateAsync_WhenSaltIsMissing_ThrowsEmptyInputException()
        {
            Func<Task> userAccount = async () =>
            {
                _ = await _authRepo.AuthenticateAsync(_userName, _password, string.Empty, CancellationToken.None);
            };

            userAccount.Should().Throw<EmptyInputException>()
                .WithMessage("salt is required");
        }
    }
}
