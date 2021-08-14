using AutoMapper;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Models;
using RecipeBook.CoreApp.Domain.UserAccounts;

namespace RecipeBook.CoreApp.Api.Features.UserAccounts.Mapping
{
    public class UserAccountProfile : Profile
    {
        public UserAccountProfile()
        {
            CreateMap<UserAccount, UserAccountDto>(MemberList.Source);
        }
    }
}
