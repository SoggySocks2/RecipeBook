using RecipeBook.CoreApp.Api.Features.UserAccounts.Models;

namespace RecipeBook.CoreAppTests.Shared.UserAccounts.Builders
{
    public class AuthenticationDtoBuilder
    {
        private string UserName { get; set; }
        private string Password { get; set; }

        public AuthenticationDtoBuilder WIthTestValues()
        {
            UserName = "Test UserName";
            Password = "Test Password";
            return this;
        }
        public AuthenticationDtoBuilder WithUserName(string userName)
        {
            UserName = userName;
            return this;
        }
        public AuthenticationDtoBuilder WithPassword (string password)
        {
            Password = password;
            return this;
        }
        public AuthenticationDto Build()
        {
            return new AuthenticationDto(){ UserName = UserName, Password = Password};
        }
    }
}
