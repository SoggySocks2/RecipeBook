using Microsoft.EntityFrameworkCore;
using RecipeBook.CoreApp.Domain.UserAccounts;
using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;
using RecipeBook.SharedKernel.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Infrastructure.Data.UserAccounts
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly CoreDbContext _dbContext;

        public UserAccountRepository(CoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Add a new user account
        /// </summary>
        /// <returns>Newly created user account</returns>
        public async Task<UserAccount> AddAsync(UserAccount userAccount, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _dbContext.AddAsync(userAccount, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return userAccount;
        }

        /// <summary>
        /// Get a specific user account
        /// </summary>
        /// <param name="id">User account id</param>
        /// <returns>User account</returns>
        public async Task<UserAccount> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _dbContext.UserAccounts
                .SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<UserAccount> UpdateAsync(UserAccount userAccount, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _dbContext.SaveChangesAsync(cancellationToken);

            return userAccount;
        }

        public async Task DeleteAsync(UserAccount userAccount, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _dbContext.UserAccounts.Remove(userAccount);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<UserAccount>> GetListAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _dbContext.UserAccounts
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Authenticate a user account
        /// </summary>
        /// <param name="userName">Authentication username</param>
        /// <param name="password">Authentication password</param>
        /// <param name="salt">Salt used to hash the authentication password</param>
        /// <returns>Authenticate user account</returns>

        public async Task<UserAccount> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(userName)) throw new EmptyInputException($"{nameof(userName)} is required");
            if (string.IsNullOrWhiteSpace(password)) throw new EmptyInputException($"{nameof(password)} is required");

            var userAccount = await _dbContext.UserAccounts.FirstOrDefaultAsync(x => x.Username == userName && x.Password == password, cancellationToken);

            if (userAccount == default)
            {
                //Authentication failed
                throw new AuthenticateException("Authentication failed");
            }

            return userAccount;
        }
    }
}
