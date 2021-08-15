using FluentAssertions;
using RecipeBook.CoreAppTests.Shared.UserAccounts.Builders;
using System;
using Xunit;

namespace RecipeBook.CoreAppTests.Domain.UnitTests.UserAccounts
{
    public class PersonTests
    {
        [Fact]
        public void Constructor_FirstNameIsNull_FirstNameIsEmptyString()
        {
            var person = new PersonBuilder().WithTestValues().WithFirstName(null).Build();
            person.FirstName.Should().Be(string.Empty);
        }

        [Fact]
        public void Constructor_LastNameIsNull_LastNameIsEmptyString()
        {
            var person = new PersonBuilder().WithTestValues().WithLastName(null).Build();
            person.LastName.Should().Be(string.Empty);
        }

        [Fact]
        public void Constructor_WithValues_ConstructsOk()
        {
            var person = new PersonBuilder().WithTestValues().Build();
            person.FirstName.Should().NotBe(null);
            person.LastName.Should().NotBe(null);
        }

        [Fact]
        public void Constructor_WithoutValues_ConstructsOk()
        {
            var firstName = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();
            var person = new PersonBuilder().WithTestValues().WithFirstName(firstName).WithLastName(lastName).Build();

            person.FirstName.Should().Be(firstName);
            person.LastName.Should().Be(lastName);
        }
    }
}
