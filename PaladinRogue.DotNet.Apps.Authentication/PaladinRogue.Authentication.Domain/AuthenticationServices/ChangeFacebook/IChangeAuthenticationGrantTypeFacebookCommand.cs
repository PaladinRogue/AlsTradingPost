using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.AuthenticationServices.ChangeFacebook
{
    public interface IChangeAuthenticationGrantTypeFacebookCommand
    {
        Task ExecuteAsync(AuthenticationGrantTypeFacebook authenticationGrantTypeGoogle,
            ChangeAuthenticationGrantTypeFacebookDdto changeAuthenticationGrantTypeFacebookDdto);
    }
}