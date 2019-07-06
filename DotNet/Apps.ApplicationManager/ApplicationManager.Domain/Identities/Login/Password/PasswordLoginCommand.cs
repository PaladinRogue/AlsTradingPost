using System.Threading.Tasks;
using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.Login.Password
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