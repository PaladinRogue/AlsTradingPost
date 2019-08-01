using System;
using System.Threading.Tasks;
using Authentication.Domain.Identities.Projections;
using Authentication.Domain.Identities.Queries;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence.Identities
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
                .FromSql($"SELECT * FROM [authentication].[AuthenticationIdentities] WHERE [TYPE] = {AuthenticationIdentityTypes.TwoFactor} AND [IdentityId] = {identityId}")
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }
    }
}