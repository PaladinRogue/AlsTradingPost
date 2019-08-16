using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaladinRogue.Authentication.Domain.Identities.Projections;
using PaladinRogue.Authentication.Domain.Identities.Queries;

namespace PaladinRogue.Authentication.Persistence.Identities
{
    public class GetTwoFactorAuthenticationIdentityByIdentityQuery : IGetTwoFactorAuthenticationIdentityByIdentityQuery
    {
        private readonly AuthenticationDbContext _authenticationDbContext;

        public GetTwoFactorAuthenticationIdentityByIdentityQuery(AuthenticationDbContext authenticationDbContext)
        {
            _authenticationDbContext = authenticationDbContext;
        }

        public Task<TwoFactorAuthenticationIdentityProjection> RunAsync(Guid identityId)
        {
            return _authenticationDbContext
                .Query<TwoFactorAuthenticationIdentityProjection>()
                .FromSql($@"SELECT * FROM [authentication].[AuthenticationIdentities]
                    WHERE [Type] = {AuthenticationIdentityTypes.TwoFactor}
                    AND [IdentityId] = {identityId}")
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}