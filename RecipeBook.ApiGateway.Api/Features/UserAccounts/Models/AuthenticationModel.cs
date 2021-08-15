using RecipeBook.CoreApp.Domain.UserAccounts;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Models
{
    /// <summary>
    /// Credentials used when attempting to authenticate
    /// </summary>
    public class AuthenticationModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
