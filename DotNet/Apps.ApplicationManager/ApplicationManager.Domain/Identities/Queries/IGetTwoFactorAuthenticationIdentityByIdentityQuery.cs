using System;
using System.Threading.Tasks;
using ApplicationManager.Domain.Identities.Projections;

namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IGetTwoFactorAuthenticationIdentityByIdentityQuery
    {
        Task<TwoFactorAuthenticationIdentityProjection> RunAsync(Guid identityId);
    }
}