using System.Threading.Tasks;
using Common.Domain.Validation;
using FluentValidation;

namespace Authentication.Domain.AuthenticationServices.CreateFacebook
{
    public class CreateAuthenticationGrantTypeFacebookCommand : ICreateAuthenticationGrantTypeFacebookCommand
    {
        private readonly IValidator<CreateAuthenticationGrantTypeFacebookDdto> _validator;

        public CreateAuthenticationGrantTypeFacebookCommand(IValidator<CreateAuthenticationGrantTypeFacebookDdto> validator)
        {
            _validator = validator;
        }

        public Task<AuthenticationGrantTypeFacebook> ExecuteAsync(CreateAuthenticationGrantTypeFacebookDdto createAuthenticationGrantTypeFacebookDdto)
        {
            _validator.ValidateAndThrow(createAuthenticationGrantTypeFacebookDdto);

            return Task.FromResult(AuthenticationGrantTypeFacebook.Create(createAuthenticationGrantTypeFacebookDdto));
        }
    }
}