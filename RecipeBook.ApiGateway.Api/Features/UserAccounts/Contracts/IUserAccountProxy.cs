using RecipeBook.ApiGateway.Api.Features.UserAccounts.Models;
using RecipeBook.SharedKernel.Responses;
using RecipeBook.SharedKernel.SharedObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Contracts
{
    public interface IUserAccountProxy
    {
        /// <summary>
        /// Add a new user acocunt
        /// </summary>
        Task<ExistingUserAccountModel> AddAsync(NewUserAccountModel userAccount, CancellationToken cancellationToken);

        /// <summary>
        /// Get an existing user account
        /// </summary>
        Task<ExistingUserAccountModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Update an existing user account
        /// </summary>
        /// <returns></returns>
        Task<ExistingUserAccountModel> UpdateAsync(ExistingUserAccountModel userAccount, CancellationToken cancellationToken);

        /// <summary>
        /// Delete an existing user account
        /// </summary>
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Get a page of existing user accounts
        /// </summary>
        /// <param name="paginationFilter">Pagination parameters defining how many and which records to return</param>
        Task<PagedResponse<List<ExistingUserAccountModel>>> GetListAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken);

        /// <summary>
        /// Authenticate against an existing user account
        /// </summary>
        Task<string> AuthenticateAsync(AuthenticationModel authenticationModel, CancellationToken cancellationToken);
    }
}
