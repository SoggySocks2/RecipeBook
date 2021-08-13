using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using RecipeBook.CoreApp.Api.Features.UserAccount.Contracts;
using RecipeBook.CoreApp.Api.Features.UserAccount.Models;
using RecipeBook.CoreApp.Domain.Account.Contracts;
using RecipeBook.SharedKernel.CustomExceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Api.Features.UserAccount.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(IMapper mapper, IUserAccountRepository userAccountRepository)
        {
            if (mapper is null)
            {
                throw new EmptyInputException($"{nameof(mapper)} is null");
            }
            if (userAccountRepository is null)
            {
                throw new EmptyInputException($"{nameof(userAccountRepository)} is required");
            }

            _mapper = mapper;
            _userAccountRepository = userAccountRepository;
        }

        public async Task<string> AuthenticateAsync(string salt, string jwtEncryptionKey, AuthDto authDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new EmptyInputException($"{nameof(salt)} is required");
            }

            if (string.IsNullOrWhiteSpace(jwtEncryptionKey))
            {
                throw new EmptyInputException($"{nameof(salt)} is jwtEncryptionKey");
            }

            if (authDto is null)
            {
                throw new EmptyInputException($"{nameof(authDto)} is required");
            }

            if (string.IsNullOrWhiteSpace(authDto.Username))
            {
                throw new EmptyInputException($"{nameof(authDto.Username)} is required");
            }

            if (string.IsNullOrWhiteSpace(authDto.Password))
            {
                throw new EmptyInputException($"{nameof(authDto.Password)} is required");
            }

            var authenticateduserAccount = await _userAccountRepository.AuthenticateAsync(authDto.Username, authDto.Password, salt, cancellationToken);

            var tokenHandler = new JwtSecurityTokenHandler();

            if (string.IsNullOrWhiteSpace(jwtEncryptionKey))
            {
                throw new EmptyInputException($"{nameof(jwtEncryptionKey)} is required");
            }

            var key = Encoding.ASCII.GetBytes(jwtEncryptionKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                            new Claim(ClaimTypes.NameIdentifier, authenticateduserAccount.Id.ToString()),
                            new Claim(ClaimTypes.Name, $"{authenticateduserAccount.Firstname} {authenticateduserAccount.Lastname}"),
                            new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public async Task<UserAccountDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new EmptyInputException($"{nameof(id)} is requried");
            }

            var userAccount = await _userAccountRepository.GetByIdAsync(id, cancellationToken);

            if (userAccount is null || userAccount.Id == Guid.Empty)
            {
                throw new NotFoundException("User account not found");
            }

            return _mapper.Map<UserAccountDto>(userAccount);
        }

        public async Task<List<UserAccountDto>> GetListAsync(CancellationToken cancellationToken)
        {
            var userAccounts = await _userAccountRepository.GetListAsync(cancellationToken);

            return _mapper.Map<List<UserAccountDto>>(userAccounts);
        }
    }
}
