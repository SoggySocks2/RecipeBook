using RecipeBook.CoreApp.Domain.UserAccounts;
using RecipeBook.CoreApp.Infrastructure.Data.UserAccounts;
using System;

namespace RecipeBook.CoreAppTests.Shared.UserAccounts.Builders
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
        public UserAccountBuilder WithPassword(string password)
        {
            Password = password;
            return this;
        }
        public UserAccountBuilder WithRole(string role)
        {
            Role = role;
            return this;
        }
        public UserAccount Build()
        {
            var person = new Person(Firstname, Lastname);
            var userAcount = new UserAccount(person, Username, Password, Role);
            userAcount.GetType().GetProperty("Id").SetValue(userAcount, Id);

            return userAcount;
        }
    }
}
