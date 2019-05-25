using FluentValidation;

namespace ApplicationManager.Domain.Identities.AuthenticationIdentities
{
    public class CreatePasswordIdentityValidator : AbstractValidator<CreatePasswordIdentityDdto>
    {
        public CreatePasswordIdentityValidator()
        {
            RuleFor(i => i.EmailAddress)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
