using RecipeBook.SharedKernel.Contracts;
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

        public UserAccountBuilder WithTestValues()
        {
            Id = Guid.NewGuid();
            Firstname = "Test Firstname";
            Lastname = "Test Lastname";
            Username = "Test Username";
            Password = "Test Password";
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
        public IUserAccount Build()
        {
            var userAcount = new CoreApp.Domain.Account.UserAccount(Firstname, Lastname, Username, Password);
            userAcount.GetType().GetProperty("Id").SetValue(userAcount, Id);

            return userAcount;
        }
    }
}
