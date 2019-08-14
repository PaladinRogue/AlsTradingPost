using System.Threading.Tasks;
using PaladinRogue.Library.Authorisation.Common.Contexts;
using PaladinRogue.Library.Authorisation.Common.Policies;

namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Policies.Deny
{
    public class AlwaysDenyAuthorisationPolicy : IAuthorisationPolicy
    {
        public Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext)
        {
            return Task.FromResult(false);
        }
    }
}
