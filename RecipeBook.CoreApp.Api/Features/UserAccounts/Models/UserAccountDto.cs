using System;

namespace RecipeBook.CoreApp.Api.Features.UserAccounts.Models
{
    public class UserAccountDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
