using System.Threading.Tasks;
using ApplicationManager.Domain.AuthenticationServices;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.RegisterClientCredential
{
    public class RegisterClientCredentialCommand : IRegisterClientCredentialCommand
    {
        private readonly IValidator<RegisterClientCredentialCommandDdto> _validator;

        public RegisterClientCredentialCommand(
            IValidator<RegisterClientCredentialCommandDdto> validator)
        {
            _validator = validator;
        }

        public Task ExecuteAsync(
            Identity identity,
            AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            RegisterClientCredentialCommandDdto registerClientCredentialCommandDdto)
        {
            _validator.ValidateAndThrow(registerClientCredentialCommandDdto);

            identity.RegisterClientCredential(authenticationGrantTypeClientCredential, new RegisterClientCredentialDdto
            {
                Identifier = registerClientCredentialCommandDdto.Identifier
            });

            return Task.CompletedTask;
        }
    }
}