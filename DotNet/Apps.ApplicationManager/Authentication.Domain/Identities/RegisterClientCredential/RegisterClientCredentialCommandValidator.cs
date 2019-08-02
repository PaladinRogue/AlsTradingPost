using Common.Resources;
using FluentValidation;

namespace Authentication.Domain.Identities.RegisterClientCredential
{
    public class RegisterClientCredentialCommandValidator : AbstractValidator<RegisterClientCredentialCommandDdto>
    {
        public RegisterClientCredentialCommandValidator()
        {
            RuleFor(p => p)
                .NotNull();

            RuleFor(l => l.Identifier)
                .MaximumLength(FieldSizes.Extended)
                .NotEmpty();
        }
    }
}