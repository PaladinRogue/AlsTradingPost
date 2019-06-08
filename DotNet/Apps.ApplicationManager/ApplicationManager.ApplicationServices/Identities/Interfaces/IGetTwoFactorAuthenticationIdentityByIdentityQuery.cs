using System;

namespace ApplicationManager.ApplicationServices.Identities.Interfaces
{
    public interface IGetTwoFactorAuthenticationIdentityByIdentityQuery
    {
        TwoFactorAuthenticationIdentityProjection Execute(Guid identityId);
    }
}