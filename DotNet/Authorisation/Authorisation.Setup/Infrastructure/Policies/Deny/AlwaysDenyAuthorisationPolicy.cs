using System.Threading.Tasks;
using Authorisation.Application.Contexts;

namespace Authorisation.Application.Policies.Deny
{
    public class AlwaysDenyAuthorisationPolicy : IAuthorisationPolicy
    {
        public Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext)
        {
            return Task.FromResult(false);
        }
    }
}
