using System.Threading.Tasks;
using FluentValidation;
using PaladinRogue.Libray.Core.Domain.Validation;

namespace PaladinRogue.Authentication.Domain.AuthenticationServices.ChangeFacebook
{
    public class ChangeAuthenticationGrantTypeFacebookCommand : IChangeAuthenticationGrantTypeFacebookCommand
    {
        private readonly IValidator<ChangeAuthenticationGrantTypeFacebookDdto> _validator;

        public ChangeAuthenticationGrantTypeFacebookCommand(IValidator<ChangeAuthenticationGrantTypeFacebookDdto> validator)
        {
            _validator = validator;
        }

        public Task ExecuteAsync(AuthenticationGrantTypeFacebook authenticationGrantTypeGoogle,
            ChangeAuthenticationGrantTypeFacebookDdto changeAuthenticationGrantTypeFacebookDdto)
        {
            _validator.ValidateAndThrow(changeAuthenticationGrantTypeFacebookDdto);

            authenticationGrantTypeGoogle.Change(changeAuthenticationGrantTypeFacebookDdto);

            return Task.CompletedTask;
        }
    }
}