using System;
using ApplicationManager.Domain.Identities.Projections;

namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IGetTwoFactorAuthenticationIdentityByIdentityQuery
    {
        TwoFactorAuthenticationIdentityProjection Execute(Guid identityId);
    }
}