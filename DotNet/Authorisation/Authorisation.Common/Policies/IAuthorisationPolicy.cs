using System.Threading.Tasks;
using PaladinRogue.Libray.Authorisation.Common.Contexts;

namespace PaladinRogue.Libray.Authorisation.Common.Policies
{
    public interface IAuthorisationPolicy
    {
        Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext);
    }
}
