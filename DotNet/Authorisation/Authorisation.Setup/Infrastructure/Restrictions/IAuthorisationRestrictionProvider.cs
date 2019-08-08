namespace Authorisation.Application.Restrictions
{
    public interface IAuthorisationRestrictionProvider
    {
        IAuthorisationRestriction GetByRestriction(string restriction);
    }
}