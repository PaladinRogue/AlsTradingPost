using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.AddTwoFactor
{
    public class AddTwoFactorAuthenticationIdentityCommand : IAddTwoFactorAuthenticationIdentityCommand
    {
        private readonly IValidator<AddTwoFactorAuthenticationIdentityDdto> _validator;

        public AddTwoFactorAuthenticationIdentityCommand(IValidator<AddTwoFactorAuthenticationIdentityDdto> validator)
        {
            _validator = validator;
        }

        public void Execute(
            Identity identity,
            AddTwoFactorAuthenticationIdentityDdto addTwoFactorAuthenticationIdentityDdto)
        {
            _validator.ValidateAndThrow(addTwoFactorAuthenticationIdentityDdto);

            identity.AddTwoFactorAuthenticationIdentity(addTwoFactorAuthenticationIdentityDdto);
        }
    }
}
