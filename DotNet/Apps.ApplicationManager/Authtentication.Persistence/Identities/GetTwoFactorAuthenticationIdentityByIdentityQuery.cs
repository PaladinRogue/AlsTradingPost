using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Domain.Identities.Projections;
using Authentication.Domain.Identities.Queries;
using Microsoft.EntityFrameworkCore;

namespace Authtentication.Persistence.Identities
{
    public class GetTwoFactorAuthenticationIdentityByIdentityQuery : IGetTwoFactorAuthenticationIdentityByIdentityQuery
    {
        private readonly AuthenticationDbContext _authenticationDbContext;

        public GetTwoFactorAuthenticationIdentityByIdentityQuery(AuthenticationDbContext authenticationDbContext)
        {
            _authenticationDbContext = authenticationDbContext;
        }

        public async Task<TwoFactorAuthenticationIdentityProjection> RunAsync(Guid identityId)
        {
            List<TwoFactorAuthenticationIdentityProjection> twoFactorAuthenticationIdentityProjections = await _authenticationDbContext.Query<TwoFactorAuthenticationIdentityProjection>()
                .FromSql($"SELECT * FROM [apps].[AuthenticationIdentities] WHERE [TYPE] = {AuthenticationIdentityTypes.TwoFactor} AND [IdentityId] = {identityId}")
                .AsNoTracking()
                .ToListAsync();

            return twoFactorAuthenticationIdentityProjections.FirstOrDefault();
        }
    }
}