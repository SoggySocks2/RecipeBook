using AutoMapper;
using Moq;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Mapping;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Models;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Services;
using RecipeBook.CoreApp.Domain.UserAccounts;
using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;
using RecipeBook.SharedKernel.Responses;
using RecipeBook.SharedKernel.SharedObjects;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RecipeBook.CoreAppTests.Shared.UserAccounts.Builders
{
    public class UserAccountServiceBuilder
    {
        public IMapper Mapper;
        private Mock<IUserAccountRepository> RepositoryMock = new();

        public UserAccountServiceBuilder WithTestValues()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserAccountProfile());
            }).CreateMapper();

            return this;
        }
        public UserAccountServiceBuilder WithMapper(IMapper mapper)
        {
            Mapper = mapper;
            return this;
        }
        public UserAccountServiceBuilder WithRepository(Mock<IUserAccountRepository> repository)
        {
            RepositoryMock = repository;
            return this;
        }
        public UserAccountServiceBuilder Setup_AddAsync(UserAccount userAccount)
        {
            RepositoryMock.Setup(m => m.AddAsync(It.IsAny<UserAccount>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userAccount);

            return this;
        }
        public UserAccountServiceBuilder Setup_GetByIdAsync(Guid id, UserAccount userAccount)
        {
            RepositoryMock.Setup(m => m.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(userAccount);

            return this;
        }

        public UserAccountServiceBuilder Setup_GetListAsync(PagedResponse<List<UserAccount>> userAccounts)
        {
            RepositoryMock.Setup(m => m.GetListAsync(It.IsAny<PaginationFilter>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userAccounts);

            return this;
        }

        public UserAccountServiceBuilder Setup_AuthenticateAsync(UserAccount userAccount)
        {
            RepositoryMock.Setup(m => m.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userAccount);

            return this;
        }

        public UserAccountServiceBuilder Setup_UpdateAsync(UserAccount userAccount)
        {
            RepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<UserAccount>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userAccount);

            return this;
        }

        public UserAccountService Build()
        {
            return new UserAccountService(Mapper, RepositoryMock?.Object);
        }
    }
}
