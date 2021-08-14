using RecipeBook.CoreApp.Domain.UserAccounts;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Models
{
    public class AuthenticationModel
    {
        public string Username { get; set; }

        string password;
        public string Password 
        { 
            get { return password; }
            set
            {
                password = UserAccount.HashPassword(value);
            }
        }
    }
}
