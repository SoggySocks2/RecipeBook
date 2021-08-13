using AutoMapper;
using RecipeBook.CoreApp.Api.Features.UserAccount.Models;
using System.Collections.Generic;

namespace RecipeBook.CoreApp.Api.Features.UserAccount.Mapping
{
    public class UserAccountProfile : Profile
    {
        public UserAccountProfile()
        {
            CreateMap<Domain.Account.UserAccount, UserAccountDto>(MemberList.Source);
        }
    }
}
