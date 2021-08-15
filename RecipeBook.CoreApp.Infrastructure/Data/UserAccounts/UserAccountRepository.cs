using Microsoft.EntityFrameworkCore;
using RecipeBook.CoreApp.Domain.UserAccounts;
using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;
using RecipeBook.SharedKernel.CustomExceptions;
using RecipeBook.SharedKernel.Extensions;
using RecipeBook.SharedKernel.Responses;
using RecipeBook.SharedKernel.SharedObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Infrastructure.Data.UserAccounts
{
    /// <summary>
    /// Provides CRUD operations against the user account table
    /// </summary>
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly CoreDbContext _dbContext;

        public UserAccountRepository(CoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserAccount> AddAsync(UserAccount userAccount, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

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
