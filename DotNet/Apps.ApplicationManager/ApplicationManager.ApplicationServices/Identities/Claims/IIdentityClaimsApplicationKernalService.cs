using System.Threading.Tasks;

namespace ApplicationManager.ApplicationServices.Identities.Claims
{
    public interface IIdentityClaimsApplicationKernalService
    {
        Task UpdateAsync(UpdateIdentityClaimAdto updateIdentityClaimAdto);
    }
}