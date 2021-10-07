using FluentValidation;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Models.Validations
{
    public class UserAccountModelValidator : AbstractValidator<UserAccountModel>
    {
        public UserAccountModelValidator()
        {
            RuleFor(x => x.Role).NotEmpty();
        }
    }
}
