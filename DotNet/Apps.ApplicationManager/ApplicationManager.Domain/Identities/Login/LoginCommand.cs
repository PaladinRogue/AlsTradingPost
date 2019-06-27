using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.Login
{
    public class LoginCommand : ILoginCommand
    {
        private readonly IGetIdentityByIdentifierAndPasswordQuery _getIdentityByIdentifierAndPasswordQuery;

        private readonly IValidator<LoginCommandDdto> _validator;

        public LoginCommand(
            IGetIdentityByIdentifierAndPasswordQuery getIdentityByIdentifierAndPasswordQuery,
            IValidator<LoginCommandDdto> validator)
        {
            _getIdentityByIdentifierAndPasswordQuery = getIdentityByIdentifierAndPasswordQuery;
            _validator = validator;
        }

        public Identity Execute(LoginCommandDdto loginCommandDdto)
        {
            _validator.ValidateAndThrow(loginCommandDdto);

            Identity identity = _getIdentityByIdentifierAndPasswordQuery.Run(loginCommandDdto.Identifier, loginCommandDdto.Password);

            if (identity == null)
            {
                throw new InvalidLoginDomainException();
            }

            identity.Login();

            return identity;
        }
    }
}