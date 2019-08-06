using System.Threading.Tasks;
using Common.Authorisation.Contexts;

namespace Common.Authorisation.Restrictions
{
    public interface IAuthorisationRestriction
    {
        string Restriction { get; }

        Task<IRestrictionResult> CheckRestrictionAsync(IAuthorisationContext authorisationContext);
    }
}