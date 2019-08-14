using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.AuthenticationServices.ChangeGoogle
{
    public interface IChangeAuthenticationGrantTypeGoogleCommand
    {
        Task ExecuteAsync(AuthenticationGrantTypeGoogle authenticationGrantTypeGoogle,
            ChangeAuthenticationGrantTypeGoogleDdto changeAuthenticationGrantTypeGoogleDdto);
    }
}