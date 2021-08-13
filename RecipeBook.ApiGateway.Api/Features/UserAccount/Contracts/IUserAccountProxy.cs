using RecipeBook.ApiGateway.Api.Features.UserAccount.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.ApiGateway.Api.Features.UserAccount.Contracts
{
    public interface IUserAccountProxy
    {
        Task<string> AuthenticateAsync(AuthModel userAccount, CancellationToken cancellationToken);
    }
}
