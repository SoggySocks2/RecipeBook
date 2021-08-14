using AutoMapper;
using Microsoft.Extensions.Configuration;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Contracts;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Models;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Contracts;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Proxies
{
    public class UserAccountProxy : IUserAccountProxy
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserAccountService _userAccountService;

        public UserAccountProxy(IConfiguration configuration, IMapper mapper, IUserAccountService authService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userAccountService = authService;
        }

        public async Task<string> AuthenticateAsync(AuthModel authModel, CancellationToken cancellationToken)
        {
            var salt = _configuration.GetValue<string>("Salt");
            var encryptionKey = _configuration.GetValue<string>("JWTEncryptionKey");

            var authDto = _mapper.Map<AuthDto>(authModel);
            var token = await _userAccountService.AuthenticateAsync(salt, encryptionKey, authDto, cancellationToken);
            return token;
        }

        public async Task<ExistingUserAccountModel> GetByIdAsync(System.Guid id, CancellationToken cancellationToken)
        {
            var userAccount = await _userAccountService.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<ExistingUserAccountModel>(userAccount);
        }

        public async Task<List<ExistingUserAccountModel>> GetListAsync(CancellationToken cancellationToken)
        {
            var userAccounts = await _userAccountService.GetListAsync(cancellationToken);

           return _mapper.Map<List<ExistingUserAccountModel>>(userAccounts);
        }
    }
}
