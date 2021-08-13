using System;

namespace RecipeBook.CoreAppTests.Shared.UserAccount.Builders
{
    public class UserAccountBuilder
    {
        private Guid Id { get; set; }
        private string Firstname { get; set; }
        private string Lastname { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        private string Role { get; set; }

        public UserAccountBuilder WithTestValues()
        {
            Id = Guid.NewGuid();
            Firstname = "Test Firstname";
            Lastname = "Test Lastname";
            Username = "Test Username";
            Password = "Test Password";
            Role = "Admin";
            return this;
        }
        public UserAccountBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }
        public UserAccountBuilder WithFirstname(string firstname)
        {
            Firstname = firstname;
            return this;
        }
        public UserAccountBuilder WithLastname(string lastname)
        {
            Lastname = lastname;
            return this;
        }
        public UserAccountBuilder WithUsername(string username)
        {
            Username = username;
            return this;
        }
        public UserAccountBuilder WithPassword(string password, string salt)
        {
            Password = CoreApp.Infrastructure.Data.Account.UserAccountRepository.HashPassword(password, salt);
            return this;
        }
        public UserAccountBuilder WithRole(string role)
        {
            Role = role;
            return this;
        }
        public CoreApp.Domain.Account.UserAccount Build()
        {
            var userAcount = new CoreApp.Domain.Account.UserAccount(Firstname, Lastname, Username, Password, Role);
            userAcount.GetType().GetProperty("Id").SetValue(userAcount, Id);

            return userAcount;
        }
    }
}
