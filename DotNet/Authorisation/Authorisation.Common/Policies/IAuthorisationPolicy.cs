using System.Threading.Tasks;
using Authorisation.Application.Contexts;

namespace Authorisation.Application.Policies
{
    public interface IAuthorisationPolicy
    {
        Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext);
    }
}
