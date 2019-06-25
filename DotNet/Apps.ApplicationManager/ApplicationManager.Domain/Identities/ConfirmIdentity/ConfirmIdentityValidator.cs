using FluentValidation;

namespace ApplicationManager.Domain.Identities.ConfirmIdentity
{
    public class ConfirmIdentityValidator : AbstractValidator<ConfirmIdentityCommandDdto>
    {
        public ConfirmIdentityValidator()
        {
            RuleFor(c => c.Token)
                .NotEmpty();
        }
    }
}