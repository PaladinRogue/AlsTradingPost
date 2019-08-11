using System.Threading.Tasks;
using PaladinRogue.Libray.Authorisation.Common.Contexts;

namespace PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Restrictions
{
    public interface IAuthorisationRestriction
    {
        string Restriction { get; }

        Task<IRestrictionResult> CheckRestrictionAsync(IAuthorisationContext authorisationContext);
    }
}