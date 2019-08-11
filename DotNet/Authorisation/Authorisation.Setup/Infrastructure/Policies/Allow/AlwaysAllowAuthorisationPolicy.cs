using System.Threading.Tasks;
using PaladinRogue.Libray.Authorisation.Common.Contexts;
using PaladinRogue.Libray.Authorisation.Common.Policies;

namespace PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Policies.Allow
{
    public class AlwaysAllowAuthorisationPolicy : IAuthorisationPolicy
    {
        public Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext)
        {
            return Task.FromResult(true);
        }
    }
}
