using System;
using System.Threading.Tasks;
using Authentication.Domain.Identities.Projections;

namespace Authentication.Domain.Identities.Queries
{
    public interface IGetTwoFactorAuthenticationIdentityByIdentityQuery
    {
        Task<TwoFactorAuthenticationIdentityProjection> RunAsync(Guid identityId);
    }
}