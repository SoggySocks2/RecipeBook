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
                .IncludeMembers(x => x.Person); /* Person is a value object so must be specifically included */

            CreateMap<Person, UserAccountDto>(MemberList.Source);

            CreateMap<UserAccountDto, UserAccount>()
                .ConvertUsing((source, dest, context) =>
                    {
                        var person = new Person(source.Firstname, source.Lastname);

                        if (dest is UserAccount userAccount)
                        {
                            // Update existing user
                            userAccount.UpdatePersonDetails(person);
                            userAccount.UpdateRole(source.Role);
                            userAccount.UpdateLoginCredentials(source.Username, source.Password);
                            return userAccount;
                        }

                        // Create a new user
                        return new UserAccount(person, source.Username, source.Password, source.Role);
                    });
        }
    }
}
