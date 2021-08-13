using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Domain.Account.Contracts
{
    public interface IUserAccountRepository
    {
        Task<UserAccount> AuthenticateAsync(string username, string password, string salt, CancellationToken cancellationToken);
        Task<Guid> AddAsync(string firstname, string lastname, string username, string password, string role, string salt, CancellationToken cancellationToken);
    }
}
