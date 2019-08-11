using System.Threading.Tasks;
using FluentValidation;
using PaladinRogue.Authentication.Domain.Identities.Queries;
using PaladinRogue.Libray.Core.Domain.Validation;

namespace PaladinRogue.Authentication.Domain.Identities.Login.Password
{
    public class PasswordLoginCommand : IPasswordLoginCommand
    {
        private readonly IGetIdentityByIdentifierAndPasswordQuery _getIdentityByIdentifierAndPasswordQuery;

        private readonly IValidator<PasswordLoginCommandDdto> _validator;

        public PasswordLoginCommand(
            IGetIdentityByIdentifierAndPasswordQuery getIdentityByIdentifierAndPasswordQuery,
            IValidator<PasswordLoginCommandDdto> validator)
        {
            _getIdentityByIdentifierAndPasswordQuery = getIdentityByIdentifierAndPasswordQuery;
            _validator = validator;
        }

        public async Task<Identity> ExecuteAsync(PasswordLoginCommandDdto passwordLoginCommandDdto)
        {
            _validator.ValidateAndThrow(passwordLoginCommandDdto);

            Identity identity = await _getIdentityByIdentifierAndPasswordQuery.RunAsync(passwordLoginCommandDdto.Identifier, passwordLoginCommandDdto.Password);

            if (identity == null)
            {
                throw new InvalidLoginDomainException();
            }

            identity.Login();

            return identity;
        }
    }
}