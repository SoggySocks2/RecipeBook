using RecipeBook.CoreApp.Domain.UserAccounts;

namespace RecipeBook.CoreAppTests.Shared.UserAccounts.Builders
{
    public class PersonBuilder
    {
        private string FirstName { get; set; }
        private string LastName { get; set; }

        public PersonBuilder WithTestValues()
        {
            FirstName = "Test First Name";
            LastName = "Test Last Name";
            return this;
        }
        public PersonBuilder WithFirstName(string firstName)
        {
            FirstName = firstName;
            return this;
        }
        public PersonBuilder WithLastName(string lastName)
        {
            LastName = lastName;
            return this;
        }
        public Person Build()
        {
            return new Person(FirstName, LastName);
        }
    }
}
