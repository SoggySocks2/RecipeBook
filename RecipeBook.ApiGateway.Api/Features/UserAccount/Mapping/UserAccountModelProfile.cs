using AutoMapper;
using RecipeBook.ApiGateway.Api.Features.UserAccount.Models;
using RecipeBook.CoreApp.Api.Features.Models;
using RecipeBook.CoreApp.Api.Features.UserAccount.Models;

namespace RecipeBook.ApiGateway.Api.Features.UserAccount.Mapping
{
    public class UserAccountModelProfile : Profile
    {
        public UserAccountModelProfile()
        {
            CreateMap<AuthModel, AuthDto>();

            CreateMap<ExistingUserAccountModel, UserAccountDto>();
            CreateMap<NewUserAccountModel, UserAccountDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserAccountDto, ExistingUserAccountModel>();
            CreateMap<UserAccountDto, NewUserAccountModel>();
        }
    }
}
