using System.Threading.Tasks;
using PaladinRogue.Library.Authorisation.Common.Contexts;

namespace PaladinRogue.Library.Authorisation.Common.Policies
{
    public interface IAuthorisationPolicy
    {
        Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext);
    }
}
