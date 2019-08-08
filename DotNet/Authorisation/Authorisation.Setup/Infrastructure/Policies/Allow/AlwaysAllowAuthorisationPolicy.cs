using System.Threading.Tasks;
using Authorisation.Application.Contexts;

namespace Authorisation.Application.Policies.Allow
{
    public class AlwaysAllowAuthorisationPolicy : IAuthorisationPolicy
    {
        public Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext)
        {
            return Task.FromResult(true);
        }
    }
}
