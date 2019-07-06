using System.Threading.Tasks;

namespace ApplicationManager.Domain.AuthenticationServices.CreateClientCredential
{
    public interface ICreateAuthenticationGrantTypeClientCredentialCommand
    {
        Task<AuthenticationGrantTypeClientCredential> ExecuteAsync(CreateAuthenticationGrantTypeClientCredentialDdto createAuthenticationGrantTypeClientCredentialDdto);
    }
}