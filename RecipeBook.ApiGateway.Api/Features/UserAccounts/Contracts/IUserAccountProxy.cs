using RecipeBook.ApiGateway.Api.Features.UserAccounts.Models;
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
        Task<List<ExistingUserAccountModel>> GetListAsync(CancellationToken cancellationToken);
        Task<string> AuthenticateAsync(AuthenticationModel authenticationModel, CancellationToken cancellationToken);
    }
}
