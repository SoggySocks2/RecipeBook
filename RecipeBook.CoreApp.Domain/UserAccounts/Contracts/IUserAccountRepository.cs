using RecipeBook.SharedKernel.Responses;
using RecipeBook.SharedKernel.SharedObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Domain.UserAccounts.Contracts
{
    public interface IUserAccountRepository
    {
        /// <summary>
        /// Add a new user account
        /// </summary>
        Task<UserAccount> AddAsync(UserAccount userAccount, CancellationToken cancellationToken);

        /// <summary>
        /// Get an existing user account
        /// </summary>
        Task<UserAccount> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Update an existing user account
        /// </summary>
        Task<UserAccount> UpdateAsync(UserAccount userAccount, CancellationToken cancellationToken);

        /// <summary>
        /// Delete an existing user account
        /// </summary>
        Task DeleteByIdAsync(UserAccount userAccount, CancellationToken cancellationToken);

        /// <summary>
        /// Get a page of user accounts
        /// </summary>
        Task<PagedResponse<List<UserAccount>>> GetListAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken);

        /// <summary>
        /// Attempt to authenticate an existing user account
        /// </summary>
        Task<UserAccount> AuthenticateAsync(string username, string password, CancellationToken cancellationToken);
    }
}
