using System.Threading.Tasks;
using Common.Authorisation.Contexts;

namespace Common.Authorisation.Policies
{
    public interface IAuthorisationPolicy
    {
        Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext);
    }
}
