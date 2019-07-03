using System;
using System.Linq;
using ApplicationManager.Domain.Identities;
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
            return _applicationManagerDbContext.Identities
                .FirstOrDefault(i => i.Id == identityId)?
                .AuthenticationIdentities
                .OfType<TwoFactorAuthenticationIdentity>()
                .Select(a => new TwoFactorAuthenticationIdentityProjection
                {
                    EmailAddress = a.EmailAddress
                })
                .FirstOrDefault();
        }
    }
}