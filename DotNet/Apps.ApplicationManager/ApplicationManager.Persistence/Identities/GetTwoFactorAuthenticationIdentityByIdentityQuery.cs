using System;
using System.Linq;
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

        public TwoFactorAuthenticationIdentityProjection Run(Guid identityId)
        {
            return _applicationManagerDbContext.Query<TwoFactorAuthenticationIdentityProjection>()
                .FromSql($"SELECT * FROM [apps].[AuthenticationIdentities] WHERE [TYPE] = {AuthenticationIdentityTypes.TwoFactor} AND [IdentityId] = {identityId}")
                .AsNoTracking()
                .ToList().FirstOrDefault();
        }
    }
}