using System.Threading.Tasks;
using Authorisation.Application.Contexts;

namespace Authorisation.Application.Restrictions
{
    public interface IAuthorisationRestriction
    {
        string Restriction { get; }

        Task<IRestrictionResult> CheckRestrictionAsync(IAuthorisationContext authorisationContext);
    }
}