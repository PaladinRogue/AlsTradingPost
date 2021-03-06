using FluentValidation;

namespace PaladinRogue.Authentication.Domain.Identities.ConfirmIdentity
{
    public class ConfirmIdentityValidator : AbstractValidator<ConfirmIdentityCommandDdto>
    {
        public ConfirmIdentityValidator()
        {
            RuleFor(c => c)
                .NotNull();

            RuleFor(c => c.Token)
                .NotEmpty();
        }
    }
}