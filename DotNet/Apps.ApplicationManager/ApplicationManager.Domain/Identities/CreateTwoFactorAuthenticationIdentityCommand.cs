using ApplicationManager.Domain.Identities.AuthenticationIdentities;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities
{
    public class CreateTwoFactorAuthenticationIdentityCommand : ICreateTwoFactorAuthenticationIdentityCommand
    {
        private readonly IValidator<CreateTwoFactorAuthenticationIdentityDdto> _validator;

        public CreateTwoFactorAuthenticationIdentityCommand(IValidator<CreateTwoFactorAuthenticationIdentityDdto> validator)
        {
            _validator = validator;
        }

        public void Execute(
            Identity identity,
            CreateTwoFactorAuthenticationIdentityDdto createTwoFactorAuthenticationIdentityDdto)
        {
            _validator.ValidateAndThrow(createTwoFactorAuthenticationIdentityDdto);

            identity.CreateTwoFactorAuthenticationIdentity(createTwoFactorAuthenticationIdentityDdto);
        }
    }
}
