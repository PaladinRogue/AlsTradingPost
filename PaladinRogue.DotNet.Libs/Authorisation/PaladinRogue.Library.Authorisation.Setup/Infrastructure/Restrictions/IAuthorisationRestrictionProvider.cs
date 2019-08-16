namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Restrictions
{
    public interface IAuthorisationRestrictionProvider
    {
        IAuthorisationRestriction GetByRestriction(string restriction);
    }
}