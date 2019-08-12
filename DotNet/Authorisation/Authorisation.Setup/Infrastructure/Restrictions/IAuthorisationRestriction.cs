using System.Threading.Tasks;
using PaladinRogue.Library.Authorisation.Common.Contexts;

namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Restrictions
{
    public interface IAuthorisationRestriction
    {
        string Restriction { get; }

        Task<IRestrictionResult> CheckRestrictionAsync(IAuthorisationContext authorisationContext);
    }
}