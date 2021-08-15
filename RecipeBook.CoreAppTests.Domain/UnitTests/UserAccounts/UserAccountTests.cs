using FluentAssertions;
using RecipeBook.CoreAppTests.Shared.UserAccounts.Builders;
using RecipeBook.SharedKernel.CustomExceptions;
using System;
using Xunit;

namespace RecipeBook.CoreAppTests.Domain.UnitTests.UserAccounts
{
    
    public class UserAccountTests
    {
        [Fact]
        public void Constructor_PersonIsNull_ThrowsEmptyInputException()
        {
            Action act = () => new UserAccountBuilder().WithTestValues().WithPerson(null).Build();

            act.Should().Throw<EmptyInputException>()
                .WithMessage("person is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_UserNameMissing_ThrowsEmptyInputException(string userName)
        {
            Action act = () => new UserAccountBuilder().WithTestValues().WithUserName(userName).Build();

            act.Should().Throw<EmptyInputException>()
                .WithMessage("username is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_PasswordMissing_ThrowsEmptyInputException(string password)
        {
            Action act = () => new UserAccountBuilder().WithTestValues().WithPassword(password).Build();

            act.Should().Throw<EmptyInputException>()
                .WithMessage("password is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_RoleMissing_ThrowsEmptyInputException(string role)
        {
            Action act = () => new UserAccountBuilder().WithTestValues().WithRole(role).Build();

            act.Should().Throw<EmptyInputException>()
                .WithMessage("role is required");
        }

        [Fact]
        public void UpdatePerson_PersonIsNull_ThrowsEmptyInputException()
        {
            var userAccount = new UserAccountBuilder().WithTestValues().Build();

            Action act = () => userAccount.UpdatePersonDetails(null);

            act.Should().Throw<EmptyInputException>()
                .WithMessage("person is required");
        }

        [Fact]
        public void UpdatePerson_PersonIsSame_DoesNotUpdatePerson()
        {
            var userAccount = new UserAccountBuilder().WithTestValues().Build();
            var person = new PersonBuilder()
                                .WithTestValues()
                                .WithFirstName(userAccount.Person.FirstName)
                                .WithLastName(userAccount.Person.LastName)
                                .Build();

            userAccount.UpdatePersonDetails(person);

            userAccount.Person.Should().Equals(person);
        }

        [Fact]
        public void UpdatePerson_PersonIsDifferent_UpdatesPerson()
        {
            var userAccount = new UserAccountBuilder().WithTestValues().Build();
            var person = new PersonBuilder()
                                .WithTestValues()
                                .Build();

            userAccount.UpdatePersonDetails(person);

            userAccount.Person.FirstName.Should().Be(person.FirstName);
            userAccount.Person.LastName.Should().Be(person.LastName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void UpdateRole_RoleIsMissing_ThrowsEmptyInputException(string role)
        {
            var userAccount = new UserAccountBuilder().WithTestValues().Build();

            Action act = () => userAccount.UpdateRole(role);

            act.Should().Throw<EmptyInputException>()
                .WithMessage("role is required");
        }

        [Fact]
        public void UpdateRole_RoleIsSame_DoesNotUpdateRole()
        {
            var userAccount = new UserAccountBuilder().WithTestValues().Build();
            var role = userAccount.Role;

            userAccount.UpdateRole(role);

            userAccount.Role.Should().Equals(role);
        }



        [Fact]
        public void UpdateRole_RoleIsDifferent_UpdatesRole()
        {
            var userAccount = new UserAccountBuilder().WithTestValues().Build();
            var role = Guid.NewGuid().ToString();

            userAccount.UpdateRole(role);

            userAccount.Role.Should().Equals(role);
        }

        //public void UpdateLoginCredentials(string username, string password)
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void UpdateCredentials_UserNameIsMissing_ThrowsEmptyInputException(string userName)
        {
            var userAccount = new UserAccountBuilder().WithTestValues().Build();

            Action act = () => userAccount.UpdateLoginCredentials(userName, Guid.NewGuid().ToString());

            act.Should().Throw<EmptyInputException>()
                .WithMessage("username is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void UpdateCredentials_PasswordIsMissing_ThrowsEmptyInputException(string password)
        {
            var userAccount = new UserAccountBuilder().WithTestValues().Build();

            Action act = () => userAccount.UpdateLoginCredentials(Guid.NewGuid().ToString(), password);

            act.Should().Throw<EmptyInputException>()
                .WithMessage("password is required");
        }

        [Fact]
        public void UpdateCredentials_ValidValues_UpdateCredentials()
        {
            var userAccount = new UserAccountBuilder().WithTestValues().Build();
            var userName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            userAccount.UpdateLoginCredentials(userName, password);

            userAccount.Username.Should().Equals(userName);
            userAccount.Password.Should().Equals(password);
        }
    }
}
