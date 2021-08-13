using System;

namespace RecipeBook.ApiGateway.Api.Features.UserAccount.Models
{
    public class ExistingUserAccountModel : UserAccountModel
    {
        public Guid Id { get; set; }
    }
}
