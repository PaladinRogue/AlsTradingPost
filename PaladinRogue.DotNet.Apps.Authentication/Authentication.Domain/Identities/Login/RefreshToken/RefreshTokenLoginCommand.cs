using System;
using System.Threading.Tasks;
using FluentValidation;
using PaladinRogue.Authentication.Domain.Identities.Queries;
using PaladinRogue.Authentication.Domain.Identities.ValidateToken;
using PaladinRogue.Library.Core.Domain.Validation;

namespace PaladinRogue.Authentication.Domain.Identities.Login.RefreshToken
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

            if (!refreshTokenLoginCommandDdto.SessionId.HasValue) throw new ArgumentNullException(nameof(refreshTokenLoginCommandDdto.SessionId));

            Identity identity = await _getIdentityBySessionQuery.RunAsync(refreshTokenLoginCommandDdto.SessionId.Value);

            if (identity == null || identity.Session.IsRevoked)
            {
                throw new InvalidLoginDomainException();
            }

            Identities.RefreshToken refreshToken = identity.Session.RefreshToken;

            if (refreshToken == null)
            {
                throw new InvalidLoginDomainException();
            }

            bool validaToken = await refreshToken.ValidateToken(new ValidateRefreshTokenDdto
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