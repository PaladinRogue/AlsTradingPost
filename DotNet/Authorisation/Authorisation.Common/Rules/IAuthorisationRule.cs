
namespace Authorisation.Application
{
    public interface IAuthorisationRule
    {
        string Resource { get; }

        string Action { get; }
    }
}
