using Common.Authorisation.Contexts;

namespace Common.Authorisation.Restrictions
{
    public interface IAuthorisationRestriction
    {
        string Restriction { get; }

        IRestrictionResult CheckRestriction(IAuthorisationContext authorisationContext);
    }
}