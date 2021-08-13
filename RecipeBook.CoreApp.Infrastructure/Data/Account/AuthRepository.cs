using Microsoft.EntityFrameworkCore;
using RecipeBook.CoreApp.Domain.Account;
using RecipeBook.CoreApp.Domain.Account.Contracts;
using RecipeBook.SharedKernel.CustomExceptions;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Infrastructure.Data.Account
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CoreDbContext _dbContext;

        public AuthRepository(CoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Add a new user account
        /// </summary>
        /// <param name="firstName">User account first name</param>
        /// <param name="lastName">User account last name</param>
        /// <param name="userName">Authentication username</param>
        /// <param name="password">Authentication password</param>
        /// <param name="salt">Salt used to hash the authentication password</param>
        /// <returns>Id the of the new user account</returns>
        public async Task<Guid> AddAsync(string firstName, string lastName, string userName, string password, string salt, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(firstName)) throw new EmptyInputException($"{nameof(firstName)} is null or empty");
            if (string.IsNullOrWhiteSpace(lastName)) throw new EmptyInputException($"{nameof(lastName)} is null or empty");
            if (string.IsNullOrWhiteSpace(userName)) throw new EmptyInputException($"{nameof(userName)} is null or empty");
            if (string.IsNullOrWhiteSpace(password)) throw new EmptyInputException($"{nameof(password)} is null or empty");
            if (string.IsNullOrWhiteSpace(salt)) throw new EmptyInputException($"{nameof(salt)} is null or empty");

            var hashedPassword = HashPassword(password, salt);

            var userAccount = new UserAccount(firstName, lastName, userName, hashedPassword);
            await _dbContext.AddAsync(userAccount, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return userAccount.Id;
        }

        /// <summary>
        /// Authenticate a user account
        /// </summary>
        /// <param name="userName">Authentication username</param>
        /// <param name="password">Authentication password</param>
        /// <param name="salt">Salt used to hash the authentication password</param>
        /// <returns>Authenticate user account</returns>

        public async Task<UserAccount> AuthenticateAsync(string userName, string password, string salt, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(userName)) throw new EmptyInputException($"{nameof(userName)} is required");
            if (string.IsNullOrWhiteSpace(password)) throw new EmptyInputException($"{nameof(password)} is required");

            var hashedPassword = HashPassword(password, salt);
            var userAccount = await _dbContext.UserAccounts.SingleOrDefaultAsync(x => x.Username == userName && x.Password == hashedPassword, cancellationToken);

            if (userAccount == default)
            {
                //Authentication failed
                throw new AuthenticationException("Authentication failed");
            }

            return userAccount;
        }

        /// <summary>
        /// Create a one way hashed password
        /// </summary>
        /// <param name="password">Password to hash</param>
        /// <param name="salt">To to use when hashing the password</param>
        /// <returns>Hashed password</returns>
        public static string HashPassword(string password, string salt)
        {
            var nIterations = 23;
            var nHash = 7;

            var saltBytes = Convert.FromBase64String(salt);

            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
        }
    }
}
