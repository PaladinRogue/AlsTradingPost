using Common.Resources;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.Login.ClientCredential
{
    public class ClientCredentialLoginCommandValidator : AbstractValidator<ClientCredentialLoginCommandDdto>
    {
        public ClientCredentialLoginCommandValidator()
        {
            RuleFor(p => p)
                .NotNull();

            RuleFor(l => l.Identifier)
                .MaximumLength(FieldSizes.Extended)
                .NotEmpty();
        }
    }
}