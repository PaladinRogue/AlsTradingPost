using ApplicationManager.Domain.AuthenticationServices.Models;

namespace ApplicationManager.Domain.AuthenticationServices.Commands
{
    public interface ICreateAuthenticationGrantTypeClientCredentialCommand
    {
        AuthenticationGrantTypeClientCredential Execute(
            CreateAuthenticationGrantTypeClientCredentialDdto createAuthenticationGrantTypeClientCredentialDdto);
    }
}