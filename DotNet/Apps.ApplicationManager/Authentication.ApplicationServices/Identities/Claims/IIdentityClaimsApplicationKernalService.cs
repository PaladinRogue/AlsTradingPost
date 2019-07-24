using System.Threading.Tasks;

namespace Authentication.ApplicationServices.Identities.Claims
{
    public interface IIdentityClaimsApplicationKernalService
    {
        Task UpdateAsync(UpdateIdentityClaimAdto updateIdentityClaimAdto);
    }
}