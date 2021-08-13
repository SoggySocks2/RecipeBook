using RecipeBook.SharedKernel.Contracts;
using RecipeBook.SharedKernel.CustomExceptions;
using System;

namespace RecipeBook.CoreApp.Domain.Account
{
    /// <summary>
    /// Represents an authenticated user
    /// </summary>
    public class UserAccount /* : IUserAccount */
    {
        public Guid Id { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        // Required by EF
        private UserAccount()
        {

        }

        public UserAccount(string firstname, string lastname, string username, string password)
        {
            if (string.IsNullOrWhiteSpace(firstname)) throw new EmptyInputException($"{nameof(firstname)} is required");
            if (string.IsNullOrWhiteSpace(lastname)) throw new EmptyInputException($"{nameof(lastname)} is required");
            if (string.IsNullOrWhiteSpace(username)) throw new EmptyInputException($"{nameof(username)} is required");
            if (string.IsNullOrWhiteSpace(password)) throw new EmptyInputException($"{nameof(password)} is required");

            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Password = password;
        }
    }
}
