using AutoMapper;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Models;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Models;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Mapping
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
