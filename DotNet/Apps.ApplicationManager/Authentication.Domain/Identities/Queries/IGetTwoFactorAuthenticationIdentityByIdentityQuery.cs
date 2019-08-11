using System;
using System.Threading.Tasks;
using PaladinRogue.Authentication.Domain.Identities.Projections;

namespace PaladinRogue.Authentication.Domain.Identities.Queries
{
    public interface IGetTwoFactorAuthenticationIdentityByIdentityQuery
    {
        Task<TwoFactorAuthenticationIdentityProjection> RunAsync(Guid identityId);
    }
}