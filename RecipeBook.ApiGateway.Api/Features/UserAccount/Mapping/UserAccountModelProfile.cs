using AutoMapper;
using RecipeBook.ApiGateway.Api.Features.UserAccount.Models;
using RecipeBook.CoreApp.Api.Features.UserAccount.Models;

namespace RecipeBook.ApiGateway.Api.Features.UserAccount.Mapping
{
    public class UserAccountModelProfile : Profile
    {
        public UserAccountModelProfile()
        {
            CreateMap<AuthModel, AuthDto>();
            //CreateMap<CoreApp.Domain.Account.UserAccount, ExistingUserAccountModel>();
            CreateMap<UserAccountDto, ExistingUserAccountModel>();
        }
    }
}
