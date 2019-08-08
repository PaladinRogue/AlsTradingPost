namespace Authorisation.Application.Restrictions
{
    public interface IRestrictionResult
    {
        bool Succeeded { get; }
    }
}