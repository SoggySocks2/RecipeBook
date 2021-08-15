using System;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Models
{
    /// <summary>
    /// Represents an existing user account
    /// </summary>
    public class ExistingUserAccountModel : UserAccountModel
    {
        public Guid Id { get; set; }
    }
}
