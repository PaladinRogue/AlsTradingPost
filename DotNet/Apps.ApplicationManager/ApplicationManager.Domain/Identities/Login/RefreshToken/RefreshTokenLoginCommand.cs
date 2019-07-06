using System.Threading.Tasks;
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

        public async Task<Identity> ExecuteAsync(RefreshTokenLoginCommandDdto refreshTokenLoginCommandDdto)
        {
            _validator.ValidateAndThrow(refreshTokenLoginCommandDdto);

            Identity identity = await _getIdentityBySessionQuery.RunAsync(refreshTokenLoginCommandDdto.SessionId);

            if (identity == null || identity.Session.IsRevoked)
            {
                throw new InvalidLoginDomainException();
            }

            Identities.RefreshToken refreshToken = identity.Session.RefreshToken;

            if (refreshToken == null)
            {
                throw new InvalidLoginDomainException();
            }

            bool validaToken = refreshToken.ValidateToken(new ValidateRefreshTokenDdto
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