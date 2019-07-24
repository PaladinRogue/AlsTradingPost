using System.Threading.Tasks;

namespace Authentication.Domain.AuthenticationServices.CreateClientCredential
{
    public interface ICreateAuthenticationGrantTypeClientCredentialCommand
    {
        Task<AuthenticationGrantTypeClientCredential> ExecuteAsync(CreateAuthenticationGrantTypeClientCredentialDdto createAuthenticationGrantTypeClientCredentialDdto);
    }
}