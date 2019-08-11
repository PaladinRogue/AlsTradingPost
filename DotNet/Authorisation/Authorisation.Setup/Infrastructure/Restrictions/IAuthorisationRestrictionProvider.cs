namespace PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Restrictions
{
    public interface IAuthorisationRestrictionProvider
    {
        IAuthorisationRestriction GetByRestriction(string restriction);
    }
}