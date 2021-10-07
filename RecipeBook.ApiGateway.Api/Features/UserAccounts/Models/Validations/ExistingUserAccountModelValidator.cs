using FluentValidation;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Models.Validations
{
    public class ExistingUserAccountModelValidator : AbstractValidator<ExistingUserAccountModel>
    {
        public ExistingUserAccountModelValidator()
        {
            Include(new UserAccountModelValidator());

            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
