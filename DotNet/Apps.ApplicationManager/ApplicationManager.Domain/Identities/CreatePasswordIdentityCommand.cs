using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.AuthenticationIdentities;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities
{
    public class CreatePasswordIdentityCommand : ICreatePasswordIdentityCommand
    {
        private readonly IValidator<CreatePasswordIdentityDdto> _validator;

        public CreatePasswordIdentityCommand(IValidator<CreatePasswordIdentityDdto> validator)
        {
            _validator = validator;
        }

        public void Create(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            CreatePasswordIdentityDdto createPasswordIdentityDdto)
        {
            _validator.ValidateAndThrow(createPasswordIdentityDdto);

            identity.CreatePasswordIdentity(authenticationGrantTypePassword, createPasswordIdentityDdto);
        }
    }

    public interface ICreatePasswordIdentityCommand
    {
        void Create(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            CreatePasswordIdentityDdto createPasswordIdentityDdto);
    }
}
