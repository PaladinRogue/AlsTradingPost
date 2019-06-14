using ApplicationManager.Domain.Identities.Models;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.Validators
{
    public class CreateTwoFactorAuthenticationIdentityValidator : AbstractValidator<AddTwoFactorAuthenticationIdentityDdto>
    {
        public CreateTwoFactorAuthenticationIdentityValidator()
        {
            RuleFor(i => i.EmailAddress)
                .MaximumLength(255)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
