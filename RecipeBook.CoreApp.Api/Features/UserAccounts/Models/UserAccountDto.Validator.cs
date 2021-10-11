using FluentValidation;

namespace RecipeBook.CoreApp.Api.Features.UserAccounts.Models
{
    public class UserAccountDtoValidator : AbstractValidator<UserAccountDto>
    {
        public UserAccountDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Role).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
