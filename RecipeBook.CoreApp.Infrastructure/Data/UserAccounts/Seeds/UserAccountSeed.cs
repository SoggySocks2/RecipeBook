using RecipeBook.CoreApp.Domain.UserAccounts;
using System.Collections.Generic;

namespace RecipeBook.CoreApp.Infrastructure.Data.UserAccounts.Seeds
{
    public static class UserAccountSeed
    {
        public static List<UserAccount> GetUserAccounts(string firstNamePrefix, string lastNamePrefix, string userNamePrefix, string hashedPpassword, string role, int count = 10)
        {
            var userAccounts = new List<UserAccount>();

            for (var i = 0; i < count; i++)
            {
                var userAccount = new UserAccount($"{firstNamePrefix}{i + 1}", $"{lastNamePrefix}{i + 1}", $"{userNamePrefix}{i + 1}", hashedPpassword, role);

                userAccounts.Add(userAccount);
            }

            return userAccounts;
        }
    }
}
