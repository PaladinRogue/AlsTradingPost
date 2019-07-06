using System.Threading.Tasks;
using Common.Authorisation.Contexts;

namespace Common.Authorisation.Policies.Deny
{
    public class AlwaysDenyAuthorisationPolicy : IAuthorisationPolicy
    {
        public Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext)
        {
            return Task.FromResult(false);
        }
    }
}
