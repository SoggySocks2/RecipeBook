using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Contracts;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Models;
using RecipeBook.CoreApp.Domain.UserAccounts;
using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;
using RecipeBook.SharedKernel.Exceptions.Helpers;
using RecipeBook.SharedKernel.Responses;
using RecipeBook.SharedKernel.SharedObjects;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Api.Features.UserAccounts.Services
{
    /// <summary>
    /// Provides access to repositories
    /// </summary>
    public class UserAccountService : IUserAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(IMapper mapper, IUserAccountRepository userAccountRepository)
        {
            Check.For.Null(mapper, nameof(mapper));
            Check.For.Null(userAccountRepository, nameof(userAccountRepository));

            _mapper = mapper;
            _userAccountRepository = userAccountRepository;
        }

        public async Task<UserAccountDto> AddAsync(UserAccountDto userAccountDto, CancellationToken cancellationToken)
        {
            Check.For.Null(userAccountDto, nameof(userAccountDto));

            var newUserAccount = _mapper.Map<UserAccount>(userAccountDto);

            var userAccount = await _userAccountRepository.AddAsync(newUserAccount, cancellationToken);

            return _mapper.Map<UserAccountDto>(userAccount);
        }

        public async Task<UserAccountDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            Check.For.NullOrEmpty(id, nameof(id));

            var userAccount = await _userAccountRepository.GetByIdAsync(id, cancellationToken);

            Check.For.NotFound(id, userAccount, nameof(userAccount));

            return _mapper.Map<UserAccountDto>(userAccount);
        }

        public async Task<UserAccountDto> UpdateAsync(UserAccountDto userAccountDto, CancellationToken cancellationToken)
        {
            Check.For.Null(userAccountDto, nameof(userAccountDto));
            Check.For.NullOrEmpty(userAccountDto.Id, nameof(userAccountDto.Id));

            var userAccount = await _userAccountRepository.GetByIdAsync(userAccountDto.Id, cancellationToken);

            Check.For.NotFound(userAccountDto.Id, userAccount, nameof(userAccount));

            _mapper.Map(userAccountDto, userAccount);
            var updatedUserAccount = await _userAccountRepository.UpdateAsync(userAccount, cancellationToken);
            return _mapper.Map<UserAccountDto>(updatedUserAccount);
        }

        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            Check.For.NullOrEmpty(id, nameof(id));

            var userAccount = await _userAccountRepository.GetByIdAsync(id, cancellationToken);

            Check.For.NotFound(id, userAccount, nameof(userAccount));

            await _userAccountRepository.DeleteByIdAsync(userAccount, cancellationToken);
        }

        public async Task<PagedResponse<List<UserAccountDto>>> GetListAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken)
        {
            var userAccounts = await _userAccountRepository.GetListAsync(paginationFilter, cancellationToken);

            var data = _mapper.Map<List<UserAccountDto>>(userAccounts.Data);

            return new PagedResponse<List<UserAccountDto>>(data, userAccounts.Pagination);
        }

        public async Task<string> AuthenticateAsync(string jwtEncryptionKey, AuthenticationDto authenticationDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Check.For.NullOrEmpty(jwtEncryptionKey, nameof(jwtEncryptionKey));
            Check.For.Null(authenticationDto, nameof(authenticationDto));
            Check.For.NullOrEmpty(authenticationDto.UserName, nameof(authenticationDto.UserName));
            Check.For.NullOrEmpty(authenticationDto.Password, nameof(authenticationDto.Password));

            var authenticatedUserAccount = await _userAccountRepository.AuthenticateAsync(authenticationDto.UserName, authenticationDto.Password, cancellationToken);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtEncryptionKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                            new Claim(ClaimTypes.NameIdentifier, authenticatedUserAccount.Id.ToString()),
                            new Claim(ClaimTypes.Name, $"{authenticatedUserAccount.Person.FirstName} {authenticatedUserAccount.Person.LastName}"),
                            new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
