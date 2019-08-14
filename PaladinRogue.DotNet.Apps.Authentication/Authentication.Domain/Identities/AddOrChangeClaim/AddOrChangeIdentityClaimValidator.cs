using FluentValidation;
using PaladinRogue.Library.Core.Common;

namespace PaladinRogue.Authentication.Domain.Identities.AddOrChangeClaim
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