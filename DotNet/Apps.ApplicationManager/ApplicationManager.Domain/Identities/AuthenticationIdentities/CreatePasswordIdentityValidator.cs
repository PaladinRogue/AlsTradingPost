using FluentValidation;

namespace ApplicationManager.Domain.Identities.AuthenticationIdentities
{
    public class CreatePasswordIdentityValidator : AbstractValidator<CreatePasswordIdentityDdto>
    {
        public CreatePasswordIdentityValidator()
        {
            RuleFor(i => i.Identifier)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(i => i.Password)
                .NotEmpty()
                .Equal(i => i.ConfirmPassword)
                .MaximumLength(40);
        }
    }
}
