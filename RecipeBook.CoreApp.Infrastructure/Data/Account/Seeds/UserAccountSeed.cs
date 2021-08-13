using RecipeBook.CoreApp.Domain.Account;
using System.Collections.Generic;

namespace RecipeBook.CoreApp.Infrastructure.Data.Account.Seeds
{
    public static class UserAccountSeed
    {
        public static List<UserAccount> GetUserAccounts(string firstNamePrefix, string lastNamePrefix, string userNamePrefix, string hashedPpassword, string role, int count = 3)
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
