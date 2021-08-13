using RecipeBook.ApiGateway.Api.Features.UserAccount.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.ApiGateway.Api.Features.UserAccount.Contracts
{
    public interface IUserAccountProxy
    {
        Task<string> AuthenticateAsync(AuthModel userAccount, CancellationToken cancellationToken);
        Task<ExistingUserAccountModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<ExistingUserAccountModel>> GetListAsync(CancellationToken cancellationToken);
    }
}
