using FluentAssertions;
using RecipeBook.CoreApp.Domain.UserAccounts;
using RecipeBook.CoreAppTests.Shared.UserAccounts.Builders;
using RecipeBook.SharedKernel.CustomExceptions;
using RecipeBook.SharedKernel.Responses;
using RecipeBook.SharedKernel.SharedObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecipeBook.CoreAppTests.Api.UnitTests
{
    public class UserAccountServiceTests
    {
        [Fact]
        public void Constructor_WithMapperNull_ThrowsEmptyInputException()
        {
            Action act = () => new UserAccountServiceBuilder().WithTestValues().WithMapper(null).Build();

            act.Should().Throw<EmptyInputException>()
                .WithMessage("mapper is required");
        }

        [Fact]
        public void Constructor_WithVehicleRepositoryNull_ThrowsEmptyInputException()
        {
            Action act = () => new UserAccountServiceBuilder().WithTestValues().WithRepository(null).Build();

            act.Should().Throw<EmptyInputException>()
                .WithMessage("userAccountRepository is required");
        }

        [Fact]
        public async Task AddAsync_WithInvalidUserAccountDto_ThrowsEmptyInputException()
        {
            var userAccountService = new UserAccountServiceBuilder().WithTestValues().Build();

            await userAccountService.Invoking(t => t.AddAsync(null, CancellationToken.None))
                .Should().ThrowAsync<EmptyInputException>()
                .WithMessage("userAccountDto is required");
        }

        [Fact]
        public async Task AddAsync_WithValidUserAccountDto_AddsUserAccount()
        {
            var id = Guid.NewGuid();
            var createdUserAccount = new UserAccountBuilder().WithTestValues().WithId(id).Build();
            var userAccountService = new UserAccountServiceBuilder().WithTestValues().Setup_AddAsync(createdUserAccount).Build();

            var userAccountDto = new UserAccountDtoBuilder().WithTestValues().Build();
            var userAccount = await userAccountService.AddAsync(userAccountDto, CancellationToken.None);

            userAccount.Id.Should().Be(id);
            userAccount.Should().Equals(createdUserAccount);
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ThrowsEmptyInputException()
        {
            var userAccountService = new UserAccountServiceBuilder().WithTestValues().Build();

            await userAccountService.Invoking(t => t.GetByIdAsync(Guid.Empty, CancellationToken.None))
                .Should().ThrowAsync<EmptyInputException>()
                .WithMessage("id is required");
        }

        [Fact]
        public async Task GetByIdAsync_WithUnknownId_ThrowsNotFoundException()
        {
            var id = Guid.NewGuid();
            var userAccountService = new UserAccountServiceBuilder().WithTestValues().Setup_GetByIdAsync(id, null).Build();
            await userAccountService.Invoking(t => t.GetByIdAsync(id, CancellationToken.None))
                .Should().ThrowAsync<NotFoundException>()
                .WithMessage("User account not found");
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_GetsUserAccount()
        {
            var id = Guid.NewGuid();
            var existingUserAccount = new UserAccountBuilder().WithTestValues().WithId(id).Build();
            var userAccountService = new UserAccountServiceBuilder().WithTestValues().Setup_GetByIdAsync(id, existingUserAccount).Build();

            var userAccount = await userAccountService.GetByIdAsync(id, CancellationToken.None);

            userAccount.Id.Should().Be(id);
            userAccount.Should().Equals(existingUserAccount);
        }


        [Fact]
        public async Task UpdateAsync_WithNullUserAccountDto_ThrowsEmptyInputException()
        {
            var userAccountService = new UserAccountServiceBuilder().WithTestValues().Build();

            await userAccountService.Invoking(t => t.UpdateAsync(null, CancellationToken.None))
                .Should().ThrowAsync<EmptyInputException>()
                .WithMessage("userAccountDto is required");
        }

        [Fact]
        public async Task UpdateAsync_WithInvalidId_ThrowsEmptyInputException()
        {
            var userAccountService = new UserAccountServiceBuilder().WithTestValues().Build();

            await userAccountService.Invoking(t => t.GetByIdAsync(Guid.Empty, CancellationToken.None))
                .Should().ThrowAsync<EmptyInputException>()
                .WithMessage("id is required");
        }

        [Fact]
        public async Task UpdateAsync_WithValidUserAccountDto_UpdatesUserAccount()
        {
            var id = Guid.NewGuid();
            var role = Guid.NewGuid().ToString();

            var userAccount = new UserAccountBuilder().WithTestValues().WithId(id).WithRole(role).Build();
            var userAccountService = new UserAccountServiceBuilder()
                                        .WithTestValues()
                                        .Setup_GetByIdAsync(id, userAccount)
                                        .Setup_UpdateAsync(userAccount)
                                        .Build();

            var userAccountDto = new UserAccountDtoBuilder().WithTestValues().WithId(id).WithRole(role).Build();
            var updatedUserAccountDto = await userAccountService.UpdateAsync(userAccountDto, CancellationToken.None);

            updatedUserAccountDto.Id.Should().Be(userAccount.Id);
            updatedUserAccountDto.Role.Should().Be(role);
        }

        [Fact]
        public async Task DeleteByIdAsync_WithInvalidId_ThrowsEmptyInputException()
        {
            var userAccountService = new UserAccountServiceBuilder().WithTestValues().Build();

            await userAccountService.Invoking(t => t.DeleteByIdAsync(Guid.Empty, CancellationToken.None))
                .Should().ThrowAsync<EmptyInputException>()
                .WithMessage("id is required");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithValidId_DeletesOk()
        {
            var id = Guid.NewGuid();
            var userAccount = new UserAccountBuilder().WithTestValues().WithId(id).Build();
            var userAccountService = new UserAccountServiceBuilder()
                                            .WithTestValues()
                                            .Setup_GetByIdAsync(userAccount.Id, userAccount)
                                            .Build();

            await userAccountService.Invoking(t => t.DeleteByIdAsync(userAccount.Id, CancellationToken.None))
                .Should().NotThrowAsync();
        }

        [Fact]
        public async Task GetListAsync_WithPageFilter_GetsPagedList()
        {
            var count = 10;
            var paginationFilter = new PaginationFilterBuilder().WithTestValues().Build();
            var pagination = new Pagination(paginationFilter, 20);

            var userAccounts = new List<UserAccount>();
            for(int i = 0; i < count; i++)
            {
                userAccounts.Add(new UserAccountBuilder().WithTestValues().Build());
            }

            var pagedResponse = new PagedResponse<List<UserAccount>>(userAccounts, pagination);

            var userAccountService = new UserAccountServiceBuilder()
                                            .WithTestValues()
                                            .Setup_GetListAsync(pagedResponse)
                                            .Build();

            var userAccountDtos = await userAccountService.GetListAsync(paginationFilter, CancellationToken.None);
            userAccountDtos.Data.Count.Should().Be(userAccounts.Count);
        }

        [Fact]
        public async Task AuthenticateAsync_WithMissingJwtEncryptionKey_ThrowsEmptyInputException()
        {
            var userAccountService = new UserAccountServiceBuilder()
                                            .WithTestValues()
                                            .Build();

            var authenticationDto = new AuthenticationDtoBuilder().WIthTestValues().Build();
            await userAccountService.Invoking(t => t.AuthenticateAsync(string.Empty, authenticationDto, CancellationToken.None))
                .Should().ThrowAsync<EmptyInputException>()
                .WithMessage("jwtEncryptionKey is required");
        }

        [Fact]
        public async Task AuthenticateAsync_WithNullAuthenticationDto_ThrowsEmptyInputException()
        {
            var userAccountService = new UserAccountServiceBuilder()
                                            .WithTestValues()
                                            .Build();

            await userAccountService.Invoking(t => t.AuthenticateAsync(Guid.NewGuid().ToString(), null, CancellationToken.None))
                .Should().ThrowAsync<EmptyInputException>()
                .WithMessage("authenticationDto is required");
        }

        [Fact]
        public async Task AuthenticateAsync_WithMissingUserName_ThrowsEmptyInputException()
        {
            var userAccountService = new UserAccountServiceBuilder()
                                            .WithTestValues()
                                            .Build();

            var authenticationDto = new AuthenticationDtoBuilder().WIthTestValues().WithUserName(string.Empty).Build();
            await userAccountService.Invoking(t => t.AuthenticateAsync(Guid.NewGuid().ToString(), authenticationDto, CancellationToken.None))
                .Should().ThrowAsync<EmptyInputException>()
                .WithMessage("Username is required");
        }

        [Fact]
        public async Task AuthenticateAsync_WithMissingPassword_ThrowsEmptyInputException()
        {
            var userAccountService = new UserAccountServiceBuilder()
                                            .WithTestValues()
                                            .Build();

            var authenticationDto = new AuthenticationDtoBuilder().WIthTestValues().WithPassword(string.Empty).Build();
            await userAccountService.Invoking(t => t.AuthenticateAsync(Guid.NewGuid().ToString(), authenticationDto, CancellationToken.None))
                .Should().ThrowAsync<EmptyInputException>()
                .WithMessage("Password is required");
        }

        [Fact]
        public async Task AuthenticateAsync_WithValidDto_ReturnsToken()
        {
            var userAccount = new UserAccountBuilder().WithTestValues().Build();
            var userAccountService = new UserAccountServiceBuilder()
                                            .WithTestValues()
                                            .Setup_AuthenticateAsync(userAccount)
                                            .Build();

            var authenticationDto = new AuthenticationDtoBuilder().WIthTestValues().Build();
            var token = await userAccountService.AuthenticateAsync(Guid.NewGuid().ToString(), authenticationDto);

            token.Should().NotBeNull();
            token.Length.Should().BeGreaterThan(0);
        }
    }
}
