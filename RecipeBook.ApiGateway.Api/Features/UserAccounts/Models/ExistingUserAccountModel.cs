using System;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Models
{
    public class ExistingUserAccountModel : UserAccountModel
    {
        public Guid Id { get; set; }
    }
}
