using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RecipeBook.CoreApp.Domain.UserAccounts;
using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;
using RecipeBook.CoreApp.Infrastructure.Data;
using RecipeBook.CoreApp.Infrastructure.Data.UserAccounts;
using RecipeBook.CoreAppTests.Shared.UserAccounts.Builders;
using RecipeBook.SharedKernel.CustomExceptions;
using System;
using System.Collections.Generic;
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

            var iConfiguration = new IConfigurationBuilder().WithTestValues().Build();
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

        [Fact]
        public async void AddAsync_WhenValid_AddsUserAccount()
        {
            var password = "Test Password";
            var testUserAccount = new UserAccountBuilder()
                                    .WithTestValues()
                                    .WithPassword(password)
                                    .Build();


            var userAccount = await _userAccountRepository.AddAsync(testUserAccount, CancellationToken.None);

            var existingUserAccount = await _userAccountRepository.GetByIdAsync(userAccount.Id, CancellationToken.None);

            existingUserAccount.Should().NotBeNull();
            existingUserAccount.Id.Should().Be(userAccount.Id);
        }

        [Fact]
        public async void GetByIdAsync_WhenIdValid_GetsUserAccount()
        {
            var password = "Test Password";
            var testUserAccount = new UserAccountBuilder()
                                    .WithTestValues()
                                    .WithPassword(password)
                                    .Build();


            var userAccount = await _userAccountRepository.AddAsync(testUserAccount, CancellationToken.None);

            var existingUserAccount = await _userAccountRepository.GetByIdAsync(userAccount.Id, CancellationToken.None);

            existingUserAccount.Should().NotBeNull();
        }

        [Fact]
        public async void UpdateAsync_WhenValid_UpdatesUserAccount()
        {
            var userName = Guid.NewGuid().ToString();
            var password = "Test Password";
            var firstName = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();
            var role = Guid.NewGuid().ToString();

            var testUserAccount = new UserAccountBuilder()
                                    .WithTestValues()
                                    .WithPassword(password)
                                    .Build();


            var userAccount = await _userAccountRepository.AddAsync(testUserAccount, CancellationToken.None);

            userAccount.UpdatePersonDetails(new Person(firstName, lastName));
            userAccount.UpdateRole(role);
            userAccount.UpdateLoginCredentials(userName, password);

            var updatedUserAccount = await _userAccountRepository.UpdateAsync(userAccount, CancellationToken.None);

            updatedUserAccount.Username.Should().Be(userName);
            updatedUserAccount.Person.FirstName.Should().Be(firstName);
            updatedUserAccount.Person.LastName.Should().Be(lastName);
            updatedUserAccount.Role.Should().Be(role);
        }


        [Fact]
        public async void DeleteByIdAsync_WhenIdValid_GetsUserAccount()
        {
            var userName = Guid.NewGuid().ToString();
            var password = "Test Password";
            var testUserAccount = new UserAccountBuilder()
                                    .WithTestValues()
                                    .WithUsername(userName)
                                    .WithPassword(password)
                                    .Build();


            var userAccount = await _userAccountRepository.AddAsync(testUserAccount, CancellationToken.None);
            var existingUserAccount = await _userAccountRepository.GetByIdAsync(userAccount.Id, CancellationToken.None);

            existingUserAccount.Should().NotBeNull();

            await _userAccountRepository.DeleteByIdAsync(userAccount, CancellationToken.None);

            existingUserAccount = await _userAccountRepository.GetByIdAsync(userAccount.Id, CancellationToken.None);

            existingUserAccount.Should().BeNull();
        }

        [Fact]
        public async void GetListAsync_WhenNoneAvailable_GetsZeroUserAccounts()
        {
            RemoveAllExistingData();

            var paginationFilter = new PaginationFilterBuilder().WithTestValues().Build();
            var pagedResponse = await _userAccountRepository.GetListAsync(paginationFilter, CancellationToken.None);
            pagedResponse.Data.Count.Should().Be(0);
        }

        [Fact]
        public async void GetListAsync_WhenAvailable_GetsAllNoneDeletedUserAccounts()
        {
            RemoveAllExistingData();

            var nonDeletedUserAccounts = new List<CoreApp.Domain.UserAccounts.UserAccount>() {
                new UserAccountBuilder().WithTestValues().Build(),
                new UserAccountBuilder().WithTestValues().Build(),
                new UserAccountBuilder().WithTestValues().Build()
            };

            var deletedUserAccounts = new List<CoreApp.Domain.UserAccounts.UserAccount>() {
                new UserAccountBuilder().WithTestValues().Build(),
                new UserAccountBuilder().WithTestValues().Build()
            };

            foreach(var userAccount in nonDeletedUserAccounts)
            {
                _ = await _userAccountRepository.AddAsync(userAccount, CancellationToken.None);
            }

            foreach (var userAccount in deletedUserAccounts)
            {
                var account = await _userAccountRepository.AddAsync(userAccount, CancellationToken.None);
                await _userAccountRepository.DeleteByIdAsync(account, CancellationToken.None);
            }

            var paginationFilter = new PaginationFilterBuilder().WithTestValues().WithPageSize(100).WithPage(1).Build();
            var pagedResponse = await _userAccountRepository.GetListAsync(paginationFilter, CancellationToken.None);

            pagedResponse.Data.Count.Should().Be(nonDeletedUserAccounts.Count);
        }

        private async void RemoveAllExistingData()
        {
            var paginationFilter = new PaginationFilterBuilder().WithTestValues().Build();

            int count = -1;
            while (count != 0)
            {
                var pagedResponse = await _userAccountRepository.GetListAsync(paginationFilter, CancellationToken.None);

                foreach(var userAccount in pagedResponse.Data)
                {
                    await _userAccountRepository.DeleteByIdAsync(userAccount, CancellationToken.None);
                }

                count = pagedResponse.Data.Count;
            }
        }
    }
}
