using RecipeBook.SharedKernel.SharedObjects;
using System.Collections.Generic;

namespace RecipeBook.CoreApp.Domain.UserAccounts
{
    public class Person : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        /* Required by EF */
        private Person() { }

        public Person(string firstName, string lastName)
        {
            /* default to empty strings */
            if (firstName is null) firstName = string.Empty;
            if (lastName is null) lastName = string.Empty;

            FirstName = firstName;
            LastName = lastName;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
