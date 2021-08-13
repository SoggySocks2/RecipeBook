using AutoMapper;
using RecipeBook.ApiGateway.Api.Features.UserAccount.Models;
using RecipeBook.CoreApp.Api.Features.UserAccount.Models;
using System.Collections.Generic;

namespace RecipeBook.ApiGateway.Api.Features.UserAccount.Mapping
{
    public class UserAccountModelProfile : Profile
    {
        public UserAccountModelProfile()
        {
            CreateMap<AuthModel, AuthDto>();
            CreateMap<UserAccountDto, ExistingUserAccountModel>();
        }
    }
}
