using FluentValidation;

namespace ApplicationManager.Domain.Identities.AddTwoFactor
{
    public class AddTwoFactorAuthenticationIdentityValidator : AbstractValidator<AddTwoFactorAuthenticationIdentityDdto>
    {
        public AddTwoFactorAuthenticationIdentityValidator()
        {
            RuleFor(i => i.EmailAddress)
                .MaximumLength(255)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
