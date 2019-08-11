
namespace PaladinRogue.Libray.Authorisation.Common.Rules
{
    public interface IAuthorisationRule
    {
        string Resource { get; }

        string Action { get; }
    }
}
