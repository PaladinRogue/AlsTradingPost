using FluentValidation;

namespace ApplicationManager.Domain.Identities.AuthenticationIdentities
{
    public class CreateTwoFactorAuthenticationIdentityValidator : AbstractValidator<CreateTwoFactorAuthenticationIdentityDdto>
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
