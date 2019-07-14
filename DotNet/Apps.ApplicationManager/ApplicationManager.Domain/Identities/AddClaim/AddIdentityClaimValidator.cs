using Common.Resources;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.AddClaim
{
    public class AddIdentityClaimValidator : AbstractValidator<AddIdentityClaimCommandDdto>
    {
        public AddIdentityClaimValidator()
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