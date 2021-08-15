using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RecipeBook.CoreApp.Domain.UserAccounts;
using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;
using RecipeBook.SharedKernel.CustomExceptions;
using RecipeBook.SharedKernel.Extensions;
using RecipeBook.SharedKernel.Responses;
using RecipeBook.SharedKernel.SharedObjects;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Infrastructure.Data.UserAccounts
{
    /// <summary>
    /// Provides CRUD operations against the user account table
    /// </summary>
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly CoreDbContext _dbContext;

        public UserAccountRepository(IConfiguration configuration, CoreDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<UserAccount> AddAsync(UserAccount userAccount, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var hashedPassword = HashPassword(userAccount.Password);
            userAccount.UpdateLoginCredentials(userAccount.UserName, hashedPassword);

            await _dbContext.AddAsync(userAccount, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return userAccount;
        }

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

        public async Task DeleteByIdAsync(UserAccount userAccount, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _dbContext.UserAccounts.Remove(userAccount);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<PagedResponse<List<UserAccount>>> GetListAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var count = await _dbContext.UserAccounts.CountAsync(cancellationToken);

            var data = await _dbContext.UserAccounts
                .ApplyOrderingAndPaging(paginationFilter, count)
                .ToListAsync(cancellationToken);

            return new PagedResponse<List<UserAccount>>(data, new Pagination(paginationFilter, count));
        }

        public async Task<UserAccount> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(userName)) throw new EmptyInputException($"{nameof(userName)} is required");
            if (string.IsNullOrWhiteSpace(password)) throw new EmptyInputException($"{nameof(password)} is required");

            var hashedPassword = HashPassword(password);

            var userAccount = await _dbContext.UserAccounts.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == hashedPassword, cancellationToken);

            if (userAccount == default)
            {
                //Authentication failed
                throw new AuthenticateException("Authentication failed");
            }

            return userAccount;
        }

        /// <summary>
        /// Create a one way hashed password
        /// </summary>
        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new EmptyInputException($"{nameof(password)} is required");

            var salt = _configuration.GetValue<string>("Salt");

            var nIterations = 23;
            var nHash = 7;

            var saltBytes = Convert.FromBase64String(salt);

            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
        }
    }
}
