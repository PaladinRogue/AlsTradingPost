using System.Threading.Tasks;

namespace ApplicationManager.Domain.AuthenticationServices.ChangeClientCredential
{
    public interface IChangeAuthenticationGrantTypeClientCredentialCommand
    {
        Task ExecuteAsync(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            ChangeAuthenticationGrantTypeClientCredentialDdto changeAuthenticationGrantTypeClientCredentialDdto);
    }
}