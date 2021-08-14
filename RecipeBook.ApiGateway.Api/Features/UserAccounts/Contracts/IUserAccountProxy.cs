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
        Task<ExistingUserAccountModel> AddAsync(NewUserAccountModel userAccount, CancellationToken cancellationToken);
        Task<ExistingUserAccountModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ExistingUserAccountModel> UpdateAsync(ExistingUserAccountModel userAccount, CancellationToken cancellationToken);
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedResponse<List<ExistingUserAccountModel>>> GetListAsync(PaginationFilter filter, CancellationToken cancellationToken);
        Task<string> AuthenticateAsync(AuthenticationModel authenticationModel, CancellationToken cancellationToken);
    }
}
