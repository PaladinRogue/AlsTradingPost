using System.Linq;
using ApplicationManager.Domain.Identities.Queries;
using ApplicationManager.Domain.Identities.ValidateToken;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.Login.RefreshToken
{
    public class RefreshTokenLoginCommand : IRefreshTokenLoginCommand
    {
        private readonly IGetIdentityBySessionQuery _getIdentityBySessionQuery;

        private readonly IValidator<RefreshTokenLoginCommandDdto> _validator;

        public RefreshTokenLoginCommand(
            IValidator<RefreshTokenLoginCommandDdto> validator,
            IGetIdentityBySessionQuery getIdentityBySessionQuery)
        {
            _validator = validator;
            _getIdentityBySessionQuery = getIdentityBySessionQuery;
        }

        public Identity Execute(RefreshTokenLoginCommandDdto refreshTokenLoginCommandDdto)
        {
            _validator.ValidateAndThrow(refreshTokenLoginCommandDdto);

            Identity identity = _getIdentityBySessionQuery.Run(refreshTokenLoginCommandDdto.SessionId);

            if (identity == null || identity.Session.IsRevoked)
            {
                throw new InvalidLoginDomainException();
            }

            RefreshTokenIdentity refreshTokenIdentity = (RefreshTokenIdentity) identity.AuthenticationIdentities.SingleOrDefault(a => a is RefreshTokenIdentity);

            if (refreshTokenIdentity == null)
            {
                throw new InvalidLoginDomainException();
            }

            bool validaToken = refreshTokenIdentity.ValidateToken(new ValidateRefreshTokenDdto
            {
                Token = refreshTokenLoginCommandDdto.RefreshToken
            });

            if (!validaToken)
            {
                throw new InvalidLoginDomainException();
            }

            identity.Login();

            return identity;
        }
    }
}