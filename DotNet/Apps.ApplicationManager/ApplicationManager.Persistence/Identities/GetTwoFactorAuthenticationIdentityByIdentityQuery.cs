using System;
using System.Linq;
using ApplicationManager.ApplicationServices.Identities.Interfaces;
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

        public TwoFactorAuthenticationIdentityProjection Execute(Guid identityId)
        {
            return _applicationManagerDbContext.Query<TwoFactorAuthenticationIdentityProjection>()
                .FromSql($"SELECT * FROM [apps].[AuthenticationIdentities] WHERE [IdentityId] = {identityId}")
                .AsNoTracking()
                .ToList().SingleOrDefault();
        }
    }
}