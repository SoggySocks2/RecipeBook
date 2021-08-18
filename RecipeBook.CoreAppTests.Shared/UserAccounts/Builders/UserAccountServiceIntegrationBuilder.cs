using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Mapping;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Services;
using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;
using RecipeBook.CoreApp.Infrastructure.Data;
using RecipeBook.CoreApp.Infrastructure.Data.UserAccounts;
using System;

namespace RecipeBook.CoreAppTests.Shared.UserAccounts.Builders
{
    public class UserAccountServiceIntegrationBuilder
    {
        private Guid DBName { get; set; }
        private IMapper Mapper { get; set; }
        private IUserAccountRepository UserAccountRepository { get; set; }

        public UserAccountServiceIntegrationBuilder WithTestValues()
        {
            DBName = Guid.NewGuid();

            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserAccountProfile());
            }).CreateMapper();


            var authenticatedUser = new AuthenticatedUserBuilder()
                        .WithTestValues()
                        .Build();

            var dbOptions = new DbContextOptionsBuilder<CoreDbContext>()
                .UseInMemoryDatabase(databaseName: DBName.ToString())
                .Options;


            var configuration = new IConfigurationBuilder().WithTestValues().Build();

            var dbContext = new CoreDbContext(dbOptions, authenticatedUser);
            UserAccountRepository = new UserAccountRepository(configuration, dbContext);

            return this;
        }
        public UserAccountServiceIntegrationBuilder WithDBName(Guid dbName)
        {
            DBName = dbName;
            return this;
        }
        public UserAccountServiceIntegrationBuilder WithMapper(IMapper mapper)
        {
            Mapper = mapper;
            return this;
        }

        public UserAccountService Build()
        {
            return new UserAccountService(Mapper, UserAccountRepository);
        }
    }
}
