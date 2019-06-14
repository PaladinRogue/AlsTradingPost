using ApplicationManager.Domain.Identities.Models;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.Validators
{
    public class CreatePasswordIdentityValidator : AbstractValidator<AddConfirmedPasswordIdentityDdto>
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
