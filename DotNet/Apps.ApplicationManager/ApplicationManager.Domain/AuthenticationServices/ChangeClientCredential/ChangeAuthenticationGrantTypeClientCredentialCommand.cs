using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.AuthenticationServices.ChangeClientCredential
{
    public class ChangeAuthenticationGrantTypeClientCredentialCommand : IChangeAuthenticationGrantTypeClientCredentialCommand
    {
        private readonly IValidator<ChangeAuthenticationGrantTypeClientCredentialDdto> _validator;

        public ChangeAuthenticationGrantTypeClientCredentialCommand(IValidator<ChangeAuthenticationGrantTypeClientCredentialDdto> validator)
        {
            _validator = validator;
        }

        public void Execute(
            AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            ChangeAuthenticationGrantTypeClientCredentialDdto changeAuthenticationGrantTypeClientCredentialDdto)
        {
            _validator.ValidateAndThrow(changeAuthenticationGrantTypeClientCredentialDdto);

            authenticationGrantTypeClientCredential.Change(changeAuthenticationGrantTypeClientCredentialDdto);
        }
    }
}