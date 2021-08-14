using AutoMapper;
using Microsoft.Extensions.Configuration;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Contracts;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Models;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Contracts;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Models;
using System;
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

        public async Task<ExistingUserAccountModel> AddAsync(NewUserAccountModel userAccount, CancellationToken cancellationToken)
        {
            var userAccountEntity = _mapper.Map<UserAccountDto>(userAccount);
            var newUserAccount = await _userAccountService.AddAsync(userAccountEntity, cancellationToken);

            return _mapper.Map<ExistingUserAccountModel>(newUserAccount);
        }

        public async Task<ExistingUserAccountModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var userAccount = await _userAccountService.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<ExistingUserAccountModel>(userAccount);
        }

        public async Task<List<ExistingUserAccountModel>> GetListAsync(CancellationToken cancellationToken)
        {
            var userAccounts = await _userAccountService.GetListAsync(cancellationToken);

           return _mapper.Map<List<ExistingUserAccountModel>>(userAccounts);
        }

        public async Task<string> AuthenticateAsync(AuthenticationModel authenticationModel, CancellationToken cancellationToken)
        {
            //var salt = _configuration.GetValue<string>("Salt");
            var encryptionKey = _configuration.GetValue<string>("JWTEncryptionKey");

            var authDto = _mapper.Map<AuthenticationDto>(authenticationModel);
            var token = await _userAccountService.AuthenticateAsync(encryptionKey, authDto, cancellationToken);
            return token;
        }
    }
}
