using RecipeBook.CoreApp.Domain.UserAccounts;
using System;

namespace RecipeBook.CoreAppTests.Shared.UserAccounts.Builders
{
    public class UserAccountBuilder
    {
        private Guid Id { get; set; }
        private Person Person { get; set; }
        private string UserName { get; set; }
        private string Password { get; set; }
        private string Role { get; set; }

        public UserAccountBuilder WithTestValues()
        {
            Id = Guid.NewGuid();
            Person = new PersonBuilder().WithTestValues().WithFirstName("Test Firstname").WithLastName("Test Lastname").Build();
            UserName = "Test Username";
            Password = "Test Password";
            Role = "Admin";
            return this;
        }
        public UserAccountBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }
        public UserAccountBuilder WithPerson(Person person)
        {
            Person = person;
            return this;
        }
        public UserAccountBuilder WithUserName(string userName)
        {
            UserName = userName;
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
            var userAcount = new UserAccount(Person, UserName, Password, Role);
            userAcount.GetType().GetProperty("Id").SetValue(userAcount, Id);

            return userAcount;
        }
    }
}
