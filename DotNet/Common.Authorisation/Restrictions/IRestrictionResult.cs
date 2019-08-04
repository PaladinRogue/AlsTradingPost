namespace Common.Authorisation.Restrictions
{
    public interface IRestrictionResult
    {
        bool Succeeded { get; }
    }
}