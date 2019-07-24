using System.Threading.Tasks;
using Common.Domain.Validation;
using FluentValidation;

namespace Authentication.Domain.AuthenticationServices.CreateClientCredential
{
    public class CreateAuthenticationGrantTypeClientCredentialCommand : ICreateAuthenticationGrantTypeClientCredentialCommand
    {
        private readonly IValidator<CreateAuthenticationGrantTypeClientCredentialDdto> _validator;

        public CreateAuthenticationGrantTypeClientCredentialCommand(IValidator<CreateAuthenticationGrantTypeClientCredentialDdto> validator)
        {
            _validator = validator;
        }

        public Task<AuthenticationGrantTypeClientCredential> ExecuteAsync(CreateAuthenticationGrantTypeClientCredentialDdto createAuthenticationGrantTypeClientCredentialDdto)
        {
            _validator.ValidateAndThrow(createAuthenticationGrantTypeClientCredentialDdto);

            return Task.FromResult(AuthenticationGrantTypeClientCredential.Create(createAuthenticationGrantTypeClientCredentialDdto));
        }
    }
}