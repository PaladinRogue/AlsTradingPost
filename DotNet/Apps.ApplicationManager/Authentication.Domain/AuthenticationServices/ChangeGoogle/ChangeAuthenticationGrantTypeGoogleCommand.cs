using System.Threading.Tasks;
using FluentValidation;
using PaladinRogue.Libray.Core.Domain.Validation;

namespace PaladinRogue.Authentication.Domain.AuthenticationServices.ChangeGoogle
{
    public class ChangeAuthenticationGrantTypeGoogleCommand : IChangeAuthenticationGrantTypeGoogleCommand
    {
        private readonly IValidator<ChangeAuthenticationGrantTypeGoogleDdto> _validator;

        public ChangeAuthenticationGrantTypeGoogleCommand(IValidator<ChangeAuthenticationGrantTypeGoogleDdto> validator)
        {
            _validator = validator;
        }

        public Task ExecuteAsync(AuthenticationGrantTypeGoogle authenticationGrantTypeGoogle,
            ChangeAuthenticationGrantTypeGoogleDdto changeAuthenticationGrantTypeGoogleDdto)
        {
            _validator.ValidateAndThrow(changeAuthenticationGrantTypeGoogleDdto);

            authenticationGrantTypeGoogle.Change(changeAuthenticationGrantTypeGoogleDdto);

            return Task.CompletedTask;
        }
    }
}