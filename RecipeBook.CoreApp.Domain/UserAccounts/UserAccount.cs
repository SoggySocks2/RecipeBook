using RecipeBook.SharedKernel.BaseClasses;
using RecipeBook.SharedKernel.CustomExceptions;
using System;
using System.Security.Cryptography;

namespace RecipeBook.CoreApp.Domain.UserAccounts
{
    /// <summary>
    /// Represents an authenticated user
    /// </summary>
    public class UserAccount : BaseEntity
    {
        public Person Person { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        // Required by EF
        private UserAccount()
        {

        }

        public UserAccount(Person person, string username, string password, string role)
        {
            if (person is null) throw new EmptyInputException($"{nameof(person)} is required");
            if (string.IsNullOrWhiteSpace(username)) throw new EmptyInputException($"{nameof(username)} is required");
            if (string.IsNullOrWhiteSpace(password)) throw new EmptyInputException($"{nameof(password)} is required");
            if (string.IsNullOrWhiteSpace(role)) throw new EmptyInputException($"{nameof(role)} is required");

            UpdatePersonDetails(person);
            Username = username;
            Password = HashPassword(password);
            Role = role;
        }

        public void UpdatePersonDetails(Person person)
        {
            if (person is null) throw new EmptyInputException($"{nameof(person)} is required");

            /* Only update if it's different so that EF doesn't perform a DB update */
            if (Person == null || !person.Equals(Person))
            {
                Person = person;
            }
        }


        public void UpdateRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role)) throw new EmptyInputException($"{nameof(role)} is required");

            /* Only update if it's different so that EF doesn't perform a DB update */
            if (Role == null || !role.Equals(Role))
            {
                Role = role;
            }
        }

        public void UpdateLoginCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new EmptyInputException($"{nameof(username)} is required");
            if (string.IsNullOrWhiteSpace(password)) throw new EmptyInputException($"{nameof(password)} is required");

            /* Only update if it's different so that EF doesn't perform a DB update */
            if (Username == null || !username.Equals(Username))
            {
                Username = username;
            }
            if (Password == null || !password.Equals(Password))
            {
                Password = HashPassword(password);
            }
        }

        /// <summary>
        /// Create a one way hashed password
        /// </summary>
        /// <param name="password">Password to hash</param>
        /// <param name="salt">To to use when hashing the password</param>
        /// <returns>Hashed password</returns>
        public static string HashPassword(string password)
        {
            //TODO: Make this dynamic
            //var salt = _configuration.GetValue<string>("Salt");
            var salt = "MDxNyrhRlHee7I0CTW9fzVk=";

            var nIterations = 23;
            var nHash = 7;

            var saltBytes = Convert.FromBase64String(salt);

            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
        }
    }
}
