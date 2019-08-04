namespace Common.Authorisation.Restrictions
{
    public interface IAuthorisationRestrictionProvider
    {
        IAuthorisationRestriction GetByRestriction(string restriction);
    }
}