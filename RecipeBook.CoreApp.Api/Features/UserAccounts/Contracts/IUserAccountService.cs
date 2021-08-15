using RecipeBook.CoreApp.Api.Features.UserAccounts.Models;
using RecipeBook.SharedKernel.Responses;
using RecipeBook.SharedKernel.SharedObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Api.Features.UserAccounts.Contracts
{
    public interface IUserAccountService
    {
        /// <summary>
        /// Add a new user account
        /// </summary>
        Task<UserAccountDto> AddAsync(UserAccountDto userAccountDto, CancellationToken cancellationToken);

        /// <summary>
        /// Get an existing user account
        /// </summary>
        Task<UserAccountDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Update an existing user account
        /// </summary>
        Task<UserAccountDto> UpdateAsync(UserAccountDto userAccountDto, CancellationToken cancellationToken);

        /// <summary>
        /// Delete an existing user account
        /// </summary>
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Get a page of user accounts
        /// </summary>
        Task<PagedResponse<List<UserAccountDto>>> GetListAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken);

        /// <summary>
        /// Attempt to authenticate a user account
        /// </summary>
        Task<string> AuthenticateAsync(string jwtEncryptionKey, AuthenticationDto authenticationDto, CancellationToken cancellationToken = default);
    }
}
