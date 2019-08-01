using System.Threading.Tasks;

namespace Authentication.Domain.AuthenticationServices.ChangeGoogle
{
    public interface IChangeAuthenticationGrantTypeGoogleCommand
    {
        Task ExecuteAsync(AuthenticationGrantTypeGoogle authenticationGrantTypeGoogle,
            ChangeAuthenticationGrantTypeGoogleDdto changeAuthenticationGrantTypeGoogleDdto);
    }
}