using AutoMapper;
using FluentAssertions;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Mapping;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Models;
using RecipeBook.CoreApp.Domain.UserAccounts;
using RecipeBook.CoreAppTests.Shared.UserAccounts.Builders;
using Xunit;

namespace RecipeBook.CoreAppTests.Api.IntegrationTests.UserAccounts
{
    public class UserAccountProfileTests
    {
        private readonly IMapper _mapper;

        public UserAccountProfileTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UserAccountProfile>());
            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void UserAccount_ToUserAccountDto()
        {
            var userAccount = new UserAccountBuilder().WithTestValues().Build();

            var userAccountDto = _mapper.Map<UserAccountDto>(userAccount);

            userAccountDto.Id.Should().Be(userAccount.Id);
            userAccountDto.FirstName.Should().Be(userAccount.Person.FirstName);
            userAccountDto.LastName.Should().Be(userAccount.Person.LastName);
            userAccountDto.Role.Should().Be(userAccount.Role);
            userAccountDto.UserName.Should().Be(userAccount.UserName);
            userAccountDto.Password.Should().Be(userAccount.Password);
        }

        [Fact]
        public void Person_ToUserAccountDto()
        {
            var person = new PersonBuilder().WithTestValues().Build();

            var userAccountDto = _mapper.Map<UserAccountDto>(person);

            userAccountDto.FirstName.Should().Be(person.FirstName);
            userAccountDto.LastName.Should().Be(person.LastName);
        }

        [Fact]
        public void UserAccountDto_ToUserAccount()
        {
            var userAccountDto = new UserAccountDtoBuilder().WithTestValues().Build();
            var userAccount = _mapper.Map<UserAccount>(userAccountDto);

            userAccount.Person.FirstName.Should().Be(userAccountDto.FirstName);
            userAccount.Person.LastName.Should().Be(userAccountDto.LastName);
            userAccount.Role.Should().Be(userAccountDto.Role);
            userAccount.UserName.Should().Be(userAccountDto.UserName);
            userAccount.Password.Should().Be(userAccountDto.Password);
        }
    }
}
