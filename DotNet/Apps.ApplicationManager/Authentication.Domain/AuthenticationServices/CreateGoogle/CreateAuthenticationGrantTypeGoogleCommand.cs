using System.Threading.Tasks;
using Common.Domain.Validation;
using FluentValidation;

namespace Authentication.Domain.AuthenticationServices.CreateGoogle
{
    public class CreateAuthenticationGrantTypeGoogleCommand : ICreateAuthenticationGrantTypeGoogleCommand
    {
        private readonly IValidator<CreateAuthenticationGrantTypeGoogleDdto> _validator;

        public CreateAuthenticationGrantTypeGoogleCommand(IValidator<CreateAuthenticationGrantTypeGoogleDdto> validator)
        {
            _validator = validator;
        }

        public Task<AuthenticationGrantTypeGoogle> ExecuteAsync(CreateAuthenticationGrantTypeGoogleDdto createAuthenticationGrantTypeGoogleDdto)
        {
            _validator.ValidateAndThrow(createAuthenticationGrantTypeGoogleDdto);

            return Task.FromResult(AuthenticationGrantTypeGoogle.Create(createAuthenticationGrantTypeGoogleDdto));
        }
    }
}