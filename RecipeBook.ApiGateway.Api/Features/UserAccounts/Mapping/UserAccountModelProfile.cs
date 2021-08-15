using AutoMapper;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Models;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Models;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Mapping
{
    public class UserAccountModelProfile : Profile
    {
        public UserAccountModelProfile()
        {
            CreateMap<ExistingUserAccountModel, UserAccountDto>();

            /* NewUserAccountModel doesn't contain an Id property so ignore it */
            CreateMap<NewUserAccountModel, UserAccountDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserAccountDto, ExistingUserAccountModel>();
            CreateMap<UserAccountDto, NewUserAccountModel>();

            CreateMap<AuthenticationModel, AuthenticationDto>();
        }
    }
}
