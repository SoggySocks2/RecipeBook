using FluentAssertions;
using Microsoft.Extensions.Configuration;
using RecipeBook.CoreAppTests.Shared.UserAccounts.Builders;
using RecipeBook.SharedKernel.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecipeBook.CoreAppTests.Api.IntegrationTests.UserAccounts
{
    public class UserAccountServiceTests
    {
        [Fact]
        public async Task AddAsync_WithValidUserAccount_CreatesUserAccount()
        {
            var userAccountService = new UserAccountServiceIntegrationBuilder().WithTestValues().Build();
            var userAccountDto = new UserAccountDtoBuilder().WithTestValues().Build();

            var createdUserAccountDto = await userAccountService.AddAsync(userAccountDto, CancellationToken.None);

            createdUserAccountDto.FirstName.Should().Be(userAccountDto.FirstName);
            createdUserAccountDto.LastName.Should().Be(userAccountDto.LastName);
            createdUserAccountDto.Role.Should().Be(userAccountDto.Role);
            createdUserAccountDto.UserName.Should().Be(userAccountDto.UserName);
        }

        [Fact]
        public async Task GetById_WhenIdIsValid_GetsUserAccountWithId()
        {
            var userAccountService = new UserAccountServiceIntegrationBuilder().WithTestValues().Build();
            var userAccountDto = new UserAccountDtoBuilder().WithTestValues().Build();

            var createdUserAccountDto = await userAccountService.AddAsync(userAccountDto, CancellationToken.None);

            var userAccount = await userAccountService.GetByIdAsync(createdUserAccountDto.Id, CancellationToken.None);

            userAccount.Id.Should().Be(createdUserAccountDto.Id);
            userAccount.FirstName.Should().Be(createdUserAccountDto.FirstName);
            userAccount.LastName.Should().Be(createdUserAccountDto.LastName);
            userAccount.Role.Should().Be(createdUserAccountDto.Role);
            userAccount.UserName.Should().Be(createdUserAccountDto.UserName);
        }

        [Fact]
        public async void Update_WhenIdExists_UpdatesStaffMember()
        {
            var userAccountService = new UserAccountServiceIntegrationBuilder().WithTestValues().Build();

            var userAccountDto = new UserAccountDtoBuilder().WithTestValues().WithId(Guid.Empty).Build();
            userAccountDto = await userAccountService.AddAsync(userAccountDto, CancellationToken.None);

            var existingUserAccountDto = await userAccountService.GetByIdAsync(userAccountDto.Id, CancellationToken.None);

            existingUserAccountDto.FirstName = Guid.NewGuid().ToString();
            existingUserAccountDto.LastName = Guid.NewGuid().ToString();
            existingUserAccountDto.Role = $"{Guid.NewGuid()}@test.com";
            existingUserAccountDto.UserName = Guid.NewGuid().ToString();

            var updatedUserAccountDto = await userAccountService.UpdateAsync(existingUserAccountDto, CancellationToken.None);

            updatedUserAccountDto.FirstName.Should().Be(existingUserAccountDto.FirstName);
            updatedUserAccountDto.LastName.Should().Be(existingUserAccountDto.LastName);
            updatedUserAccountDto.UserName.Should().Be(existingUserAccountDto.UserName);
            updatedUserAccountDto.UserName.Should().Be(existingUserAccountDto.UserName);
        }

        [Fact]
        public async Task DeleteById_WhenIdExists_NoException()
        {
            var userAccountService = new UserAccountServiceIntegrationBuilder().WithTestValues().Build();
            var userAccountDto = new UserAccountDtoBuilder().WithTestValues().Build();

            userAccountDto = await userAccountService.AddAsync(userAccountDto, CancellationToken.None);

            await userAccountService.Invoking(t => t.DeleteByIdAsync(userAccountDto.Id, CancellationToken.None))
                .Should().NotThrowAsync();

            await userAccountService.Invoking(t => t.GetByIdAsync(userAccountDto.Id, CancellationToken.None))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetListAsync_GetsUserAccounts()
        {
            var count = 19;
            var userAccountService = new UserAccountServiceIntegrationBuilder().WithTestValues().Build();

            for(int i = 0; i < count; i++)
            {
                var userAccountDto = new UserAccountDtoBuilder().WithTestValues().Build();
                await userAccountService.AddAsync(userAccountDto, CancellationToken.None);
            }

            var paginationFilter = new PaginationFilterBuilder().WithTestValues().WithPageSize(5).WithPage(4).Build();

            var response = await userAccountService.GetListAsync(paginationFilter, CancellationToken.None);

            response.Data.Count.Should().Be(4);
            response.Pagination.TotalItems.Should().Be(count);
        }

        [Fact]
        public async Task AuthenticateAsync_WithValidCredentials_AuthenticatesOK()
        {
            var userName = Guid.NewGuid().ToString();
            var password = "TestPassword";

            var userAccountService = new UserAccountServiceIntegrationBuilder().WithTestValues().Build();
            var userAccountDto = new UserAccountDtoBuilder().WithTestValues().WithUserName(userName).WithPassword(password).Build();

            await userAccountService.AddAsync(userAccountDto, CancellationToken.None);

            var configuration = new Shared.UserAccounts.Builders.IConfigurationBuilder().WithTestValues().Build();
            var encryptionKey = configuration.GetValue<string>("JWTEncryptionKey");

            var authenticationDto = new AuthenticationDtoBuilder().WIthTestValues().WithUserName(userName).WithPassword(password).Build();
            var authenticationToken = await userAccountService.AuthenticateAsync(encryptionKey, authenticationDto, CancellationToken.None);

            authenticationToken.Should().NotBeNullOrEmpty();
            authenticationToken.Length.Should().BeGreaterThan(0);
        }
    }
}
