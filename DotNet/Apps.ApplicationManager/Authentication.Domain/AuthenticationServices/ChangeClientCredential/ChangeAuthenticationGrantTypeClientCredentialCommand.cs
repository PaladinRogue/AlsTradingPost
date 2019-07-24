using System.Threading.Tasks;
using Common.Domain.Validation;
using FluentValidation;

namespace Authentication.Domain.AuthenticationServices.ChangeClientCredential
{
    public class ChangeAuthenticationGrantTypeClientCredentialCommand : IChangeAuthenticationGrantTypeClientCredentialCommand
    {
        private readonly IValidator<ChangeAuthenticationGrantTypeClientCredentialDdto> _validator;

        public ChangeAuthenticationGrantTypeClientCredentialCommand(IValidator<ChangeAuthenticationGrantTypeClientCredentialDdto> validator)
        {
            _validator = validator;
        }

        public Task ExecuteAsync(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            ChangeAuthenticationGrantTypeClientCredentialDdto changeAuthenticationGrantTypeClientCredentialDdto)
        {
            _validator.ValidateAndThrow(changeAuthenticationGrantTypeClientCredentialDdto);

            authenticationGrantTypeClientCredential.Change(changeAuthenticationGrantTypeClientCredentialDdto);

            return Task.CompletedTask;
        }
    }
}