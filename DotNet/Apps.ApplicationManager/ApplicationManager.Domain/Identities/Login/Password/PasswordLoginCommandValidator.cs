using Common.Resources;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.Login.Password
{
    public class PasswordLoginCommandValidator : AbstractValidator<PasswordLoginCommandDdto>
    {
        public PasswordLoginCommandValidator()
        {
            RuleFor(p => p)
                .NotNull();

            RuleFor(l => l.Identifier)
                .MaximumLength(FieldSizes.Extended)
                .NotEmpty();

            RuleFor(l => l.Password)
                .MaximumLength(FieldSizes.Default)
                .NotEmpty();
        }
    }
}