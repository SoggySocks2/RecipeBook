using RecipeBook.CoreApp.Api.Features.UserAccount.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Api.Features.UserAccount.Contracts
{
    public interface IUserAccountService
    {
        Task<string> AuthenticateAsync(string salt, string jwtEncryptionKey, AuthDto authDto, CancellationToken cancellationToken = default);
        Task<UserAccountDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<UserAccountDto>> GetListAsync(CancellationToken cancellationToken);
    }
}
