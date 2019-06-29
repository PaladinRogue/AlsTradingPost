using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.AuthenticationServices.CreateClientCredential
{
    public class CreateAuthenticationGrantTypeClientCredentialCommand : ICreateAuthenticationGrantTypeClientCredentialCommand
    {
        private readonly IValidator<CreateAuthenticationGrantTypeClientCredentialDdto> _validator;

        public CreateAuthenticationGrantTypeClientCredentialCommand(IValidator<CreateAuthenticationGrantTypeClientCredentialDdto> validator)
        {
            _validator = validator;
        }

        public AuthenticationGrantTypeClientCredential Execute(
            CreateAuthenticationGrantTypeClientCredentialDdto createAuthenticationGrantTypeClientCredentialDdto)
        {
            _validator.ValidateAndThrow(createAuthenticationGrantTypeClientCredentialDdto);

            return AuthenticationGrantTypeClientCredential.Create(createAuthenticationGrantTypeClientCredentialDdto);
        }
    }
}