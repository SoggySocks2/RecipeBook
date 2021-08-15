using RecipeBook.CoreApp.Api.Features.UserAccounts.Models;
using System;

namespace RecipeBook.CoreAppTests.Shared.UserAccounts.Builders
{
    public class UserAccountDtoBuilder
    {
        private Guid Id { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Role { get; set; }
        private string UserName { get; set; }
        private string Password { get; set; }

        public UserAccountDtoBuilder WithTestValues()
        {
            Id = Guid.NewGuid();
            FirstName = "Test First Name";
            LastName = "Test Last Name";
            Role = "Test Role";
            UserName = "Test User Name";
            Password = "Test Password";
            return this;
        }
        public UserAccountDtoBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }
        public UserAccountDtoBuilder WithFirstName(string firstName)
        {
            FirstName = firstName;
            return this;
        }
        public UserAccountDtoBuilder WithLastName(string lastName)
        {
            LastName = lastName;
            return this;
        }
        public UserAccountDtoBuilder WithRole(string role)
        {
            Role = role;
            return this;
        }
        public UserAccountDtoBuilder WithUserName(string userName)
        {
            UserName = userName;
            return this;
        }
        public UserAccountDtoBuilder WithPassword(string password)
        {
            Password = password;
            return this;
        }
        public UserAccountDto Build()
        {
            var userAccountDto = new UserAccountDto()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Role = Role,
                UserName = UserName,
                Password = Password
            };
            return userAccountDto;
        }
    }
}
