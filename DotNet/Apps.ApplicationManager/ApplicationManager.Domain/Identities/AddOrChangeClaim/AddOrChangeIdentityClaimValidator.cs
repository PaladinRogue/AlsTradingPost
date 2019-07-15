using Common.Resources;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.AddOrChangeClaim
{
    public class AddOrChangeIdentityClaimValidator : AbstractValidator<AddOrChangeIdentityClaimCommandDdto>
    {
        public AddOrChangeIdentityClaimValidator()
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