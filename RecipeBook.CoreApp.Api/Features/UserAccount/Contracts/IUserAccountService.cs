using RecipeBook.CoreApp.Api.Features.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Api.Features.UserAccount.Contracts
{
    public interface IUserAccountService
    {
        Task<string> AuthenticateAsync(string salt, string jwtEncryptionKey, AuthDto authDto, CancellationToken cancellationToken = default);
    }
}
