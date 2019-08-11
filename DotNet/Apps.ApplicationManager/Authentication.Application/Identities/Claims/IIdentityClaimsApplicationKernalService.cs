using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Application.Identities.Claims
{
    public interface IIdentityClaimsApplicationKernalService
    {
        Task UpdateAsync(UpdateIdentityClaimAdto updateIdentityClaimAdto);
    }
}