using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;
using RecipeBook.CoreApp.Infrastructure.Data;
using RecipeBook.CoreApp.Infrastructure.Data.UserAccounts;
using RecipeBook.CoreAppTests.Shared.UserAccounts.Builders;
using RecipeBook.SharedKernel.CustomExceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecipeBook.CoreAppTests.Infrastructure.UnitTests.UserAccounts
{
    public class UserAccountRepositoryTests
    {
        private readonly CoreDbContext _dbContext;
        private readonly IUserAccountRepository _userAccountRepository;

        private readonly string _userName = $"Test Username {Guid.NewGuid()}";
        private readonly string _password = Guid.NewGuid().ToString();

        public UserAccountRepositoryTests()
        {
            var dbOptions = new DbContextOptionsBuilder<CoreDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipeBook")
                .Options;

            var authenticatedUser = new AuthenticatedUserBuilder().WithTestValues().Build();
            _dbContext = new CoreDbContext(dbOptions, authenticatedUser);

            var iConfiguration = new IConfigurationBuilder().WithTestValues().Build();
            _userAccountRepository = new UserAccountRepository(iConfiguration, _dbContext);
        }

        [Fact]
        public void AuthenticateAsync_WhenUserNameIsMissing_ThrowsEmptyInputException()
        {
            Func<Task> userAccount = async () =>
            {
                _ = await _userAccountRepository.AuthenticateAsync(string.Empty, _password, CancellationToken.None);
            };

            userAccount.Should().Throw<EmptyInputException>()
                .WithMessage("userName is required");
        }

        [Fact]
        public void AuthenticateAsync_WhenPasswordIsMissing_ThrowsEmptyInputException()
        {
            Func<Task> userAccount = async () =>
            {
                _ = await _userAccountRepository.AuthenticateAsync(_userName, string.Empty, CancellationToken.None);
            };

            userAccount.Should().Throw<EmptyInputException>()
                .WithMessage("password is required");
        }

        [Fact]
        public void HashPassword_WhenPasswordIsMissing_ThrowsEmptyInputException()
        {
            var repo = (UserAccountRepository)_userAccountRepository;

            Action act = () => repo.HashPassword(string.Empty);

            act.Should().Throw<EmptyInputException>()
                .WithMessage("password is required");
        }

        [Theory]
        [InlineData("Passord1")]
        [InlineData("sasJJ12318")]
        [InlineData("!@#&4%£/.")]
        public void HashPassword_WhenPasswordPresent_HashesPassword(string password)
        {
            var repo = (UserAccountRepository)_userAccountRepository;
            var hashedPassword1 = repo.HashPassword(password);
            var hashedPassword2 = repo.HashPassword(password);

            hashedPassword1.Should().Equals(hashedPassword2);
        }
    }
}
