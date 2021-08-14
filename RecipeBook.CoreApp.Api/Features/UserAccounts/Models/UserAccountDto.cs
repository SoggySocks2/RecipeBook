using System;

namespace RecipeBook.CoreApp.Api.Features.UserAccounts.Models
{
    public class UserAccountDto
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
    }
}
