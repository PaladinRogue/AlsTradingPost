using System.Threading.Tasks;
using PaladinRogue.Libray.Authorisation.Common.Contexts;
using PaladinRogue.Libray.Authorisation.Common.Policies;

namespace PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Policies.Deny
{
    public class AlwaysDenyAuthorisationPolicy : IAuthorisationPolicy
    {
        public Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext)
        {
            return Task.FromResult(false);
        }
    }
}
