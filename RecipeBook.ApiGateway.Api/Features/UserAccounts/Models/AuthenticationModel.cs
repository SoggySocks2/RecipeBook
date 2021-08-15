using RecipeBook.CoreApp.Domain.UserAccounts;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Models
{
    /// <summary>
    /// Credentials used when attempting to authenticate
    /// </summary>
    public class AuthenticationModel
    {
        public string Username { get; set; }

        string password;
        public string Password 
        { 
            get { return password; }
            set
            {
                // Password is stored in db using a one way hash so we need to hash before attempting to authenticate
                password = UserAccount.HashPassword(value);
            }
        }
    }
}
