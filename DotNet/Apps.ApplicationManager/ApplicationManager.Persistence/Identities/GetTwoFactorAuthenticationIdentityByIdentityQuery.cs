using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationManager.Domain.Identities.Projections;
using ApplicationManager.Domain.Identities.Queries;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManager.Persistence.Identities
{
    public class GetTwoFactorAuthenticationIdentityByIdentityQuery : IGetTwoFactorAuthenticationIdentityByIdentityQuery
    {
        private readonly ApplicationManagerDbContext _applicationManagerDbContext;

        public GetTwoFactorAuthenticationIdentityByIdentityQuery(ApplicationManagerDbContext applicationManagerDbContext)
        {
            _applicationManagerDbContext = applicationManagerDbContext;
        }

        public async Task<TwoFactorAuthenticationIdentityProjection> RunAsync(Guid identityId)
        {
            List<TwoFactorAuthenticationIdentityProjection> twoFactorAuthenticationIdentityProjections = await _applicationManagerDbContext.Query<TwoFactorAuthenticationIdentityProjection>()
                .FromSql($"SELECT * FROM [apps].[AuthenticationIdentities] WHERE [TYPE] = {AuthenticationIdentityTypes.TwoFactor} AND [IdentityId] = {identityId}")
                .AsNoTracking()
                .ToListAsync();

            return twoFactorAuthenticationIdentityProjections.FirstOrDefault();
        }
    }
}