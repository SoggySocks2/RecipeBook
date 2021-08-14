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
        Task<UserAccountDto> AddAsync(UserAccountDto userAccountDto, CancellationToken cancellationToken);
        Task<UserAccountDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<UserAccountDto> UpdateAsync(UserAccountDto userAccountDto, CancellationToken cancellationToken);
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedResponse<List<UserAccountDto>>> GetListAsync(PaginationFilter filter, CancellationToken cancellationToken);
        Task<string> AuthenticateAsync(string jwtEncryptionKey, AuthenticationDto authenticationDto, CancellationToken cancellationToken = default);
    }
}
