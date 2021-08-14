using AutoMapper;
using RecipeBook.CoreApp.Api.Features.UserAccounts.Models;
using RecipeBook.CoreApp.Domain.UserAccounts;

namespace RecipeBook.CoreApp.Api.Features.UserAccounts.Mapping
{
    public class UserAccountProfile : Profile
    {
        public UserAccountProfile()
        {
            CreateMap<UserAccount, UserAccountDto>()
                .IncludeMembers(x => x.Person);

            CreateMap<Person, UserAccountDto>(MemberList.Source);

            CreateMap<UserAccountDto, UserAccount>()
                .ConvertUsing((source, dest, context) =>
                    {
                        var person = new Person(source.Firstname, source.Lastname);

                        if (dest is UserAccount userAccount)
                        {
                            userAccount.UpdatePersonDetails(person);
                            userAccount.UpdateRole(source.Role);
                            userAccount.UpdateLoginCredentials(source.Username, source.Password);
                            return userAccount;
                        }

                        return new UserAccount(person, source.Username, source.Password, source.Role);
                    });
        }
    }
}
