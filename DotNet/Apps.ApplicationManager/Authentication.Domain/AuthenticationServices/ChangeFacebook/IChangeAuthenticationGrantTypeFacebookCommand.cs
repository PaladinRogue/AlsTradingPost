using System.Threading.Tasks;

namespace Authentication.Domain.AuthenticationServices.ChangeFacebook
{
    public interface IChangeAuthenticationGrantTypeFacebookCommand
    {
        Task ExecuteAsync(AuthenticationGrantTypeFacebook authenticationGrantTypeGoogle,
            ChangeAuthenticationGrantTypeFacebookDdto changeAuthenticationGrantTypeFacebookDdto);
    }
}