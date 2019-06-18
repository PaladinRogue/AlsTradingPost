using Common.Resources;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommandDdto>
    {
        public LoginCommandValidator()
        {
            RuleFor(l => l.Identifier)
                .MaximumLength(FieldSizes.Extended)
                .NotEmpty();

            RuleFor(l => l.Password)
                .MaximumLength(FieldSizes.Default)
                .NotEmpty();
        }
    }
}