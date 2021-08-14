using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Contracts;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Models;
using RecipeBook.CoreApp.Domain.UserAccounts;
using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;
using RecipeBook.SharedKernel.CustomExceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Api.Features.UserAccounts.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(IMapper mapper, IUserAccountRepository userAccountRepository)
        {
            if (mapper is null) throw new EmptyInputException($"{nameof(mapper)} is null");
            if (userAccountRepository is null) throw new EmptyInputException($"{nameof(userAccountRepository)} is required");

            _mapper = mapper;
            _userAccountRepository = userAccountRepository;
        }

        public async Task<UserAccountDto> AddAsync(UserAccountDto userAccountDto, CancellationToken cancellationToken)
        {
            if (userAccountDto is null) throw new EmptyInputException($"{nameof(userAccountDto)} is required");

            var newUserAccount = _mapper.Map<UserAccount>(userAccountDto);

            await _userAccountRepository.AddAsync(newUserAccount, cancellationToken);

            return _mapper.Map<UserAccountDto>(newUserAccount);
        }

        public async Task<UserAccountDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty) throw new EmptyInputException($"{nameof(id)} is requried");

            var userAccount = await _userAccountRepository.GetByIdAsync(id, cancellationToken);

            if (userAccount is null || userAccount.Id == Guid.Empty) throw new NotFoundException("User account not found");

            return _mapper.Map<UserAccountDto>(userAccount);
        }

        public async Task<List<UserAccountDto>> GetListAsync(CancellationToken cancellationToken)
        {
            var userAccounts = await _userAccountRepository.GetListAsync(cancellationToken);

            return _mapper.Map<List<UserAccountDto>>(userAccounts);
        }

        public async Task<string> AuthenticateAsync(string jwtEncryptionKey, AuthenticationDto authenticationDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(jwtEncryptionKey)) throw new EmptyInputException($"{nameof(jwtEncryptionKey)} is jwtEncryptionKey");
            if (authenticationDto is null) throw new EmptyInputException($"{nameof(authenticationDto)} is required");
            if (string.IsNullOrWhiteSpace(authenticationDto.Username)) throw new EmptyInputException($"{nameof(authenticationDto.Username)} is required");
            if (string.IsNullOrWhiteSpace(authenticationDto.Password)) throw new EmptyInputException($"{nameof(authenticationDto.Password)} is required");

            var authenticatedUserAccount = await _userAccountRepository.AuthenticateAsync(authenticationDto.Username, authenticationDto.Password, cancellationToken);

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
