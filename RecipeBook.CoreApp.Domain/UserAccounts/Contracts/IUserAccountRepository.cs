using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Domain.UserAccounts.Contracts
{
    public interface IUserAccountRepository
    {
        Task<UserAccount> AddAsync(UserAccount userAccount, CancellationToken cancellationToken);
        Task<UserAccount> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<UserAccount>> GetListAsync(CancellationToken cancellationToken);
        Task<UserAccount> AuthenticateAsync(string username, string password, CancellationToken cancellationToken);
    }
}
