using System.Threading.Tasks;

namespace Authentication.Domain.AuthenticationServices.ChangeClientCredential
{
    public interface IChangeAuthenticationGrantTypeClientCredentialCommand
    {
        Task ExecuteAsync(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            ChangeAuthenticationGrantTypeClientCredentialDdto changeAuthenticationGrantTypeClientCredentialDdto);
    }
}