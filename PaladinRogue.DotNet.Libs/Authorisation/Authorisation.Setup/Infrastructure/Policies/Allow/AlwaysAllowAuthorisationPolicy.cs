using System.Threading.Tasks;
using PaladinRogue.Library.Authorisation.Common.Contexts;
using PaladinRogue.Library.Authorisation.Common.Policies;

namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Policies.Allow
{
    public class AlwaysAllowAuthorisationPolicy : IAuthorisationPolicy
    {
        public Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext)
        {
            return Task.FromResult(true);
        }
    }
}
