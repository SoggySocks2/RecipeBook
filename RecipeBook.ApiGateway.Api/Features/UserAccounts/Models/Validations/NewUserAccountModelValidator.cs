using FluentValidation;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Models.Validations
{
    public class NewUserAccountModelValidator : AbstractValidator<NewUserAccountModel>
    {
        public NewUserAccountModelValidator()
        {
            Include(new UserAccountModelValidator());
        }
    }
}
