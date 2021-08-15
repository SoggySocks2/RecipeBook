using RecipeBook.SharedKernel.Responses;
using RecipeBook.SharedKernel.SharedObjects;
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
        Task<UserAccount> UpdateAsync(UserAccount userAccount, CancellationToken cancellationToken);
        Task DeleteAsync(UserAccount userAccount, CancellationToken cancellationToken);
        Task<PagedResponse<List<UserAccount>>> GetListAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken);
        Task<UserAccount> AuthenticateAsync(string username, string password, CancellationToken cancellationToken);
    }
}
