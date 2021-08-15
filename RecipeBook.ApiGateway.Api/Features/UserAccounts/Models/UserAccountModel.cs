namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Models
{
    /// <summary>
    /// Properties common to both new and existing user accounts
    /// </summary>
    public class UserAccountModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
