using System.Threading.Tasks;
using Common.Authorisation.Contexts;

namespace Common.Authorisation.Policies.Allow
{
    public class AlwaysAllowAuthorisationPolicy : IAuthorisationPolicy
    {
        public Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext)
        {
            return Task.FromResult(true);
        }
    }
}
