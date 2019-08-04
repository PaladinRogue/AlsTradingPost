
namespace Common.Authorisation
{
    public interface IAuthorisationRule
    {
        string Resource { get; }

        string Action { get; }
    }
}
