using Common.Resources;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.ChangeClaim
{
    public class ChangeIdentityClaimValidator : AbstractValidator<ChangeIdentityClaimCommandDdto>
    {
        public ChangeIdentityClaimValidator()
        {
            RuleFor(c => c)
                .NotNull();

            RuleFor(i => i.Type)
                .NotEmpty()
                .MaximumLength(FieldSizes.Default);

            RuleFor(i => i.Value)
                .NotEmpty()
                .MaximumLength(FieldSizes.Default);
        }
    }
}