using System.Threading.Tasks;

namespace Authentication.Application.Identities.Claims
{
    public interface IIdentityClaimsApplicationKernalService
    {
        Task UpdateAsync(UpdateIdentityClaimAdto updateIdentityClaimAdto);
    }
}