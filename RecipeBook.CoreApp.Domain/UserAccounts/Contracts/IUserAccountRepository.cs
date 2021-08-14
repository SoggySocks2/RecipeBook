using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Domain.UserAccounts.Contracts
{
    public interface IUserAccountRepository
    {
        Task<UserAccount> AuthenticateAsync(string username, string password, string salt, CancellationToken cancellationToken);
        Task<Guid> AddAsync(string firstname, string lastname, string username, string password, string role, string salt, CancellationToken cancellationToken);
        Task<UserAccount> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<UserAccount>> GetListAsync(CancellationToken cancellationToken);
    }
}
