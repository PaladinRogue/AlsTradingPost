using ApplicationManager.Domain.AuthenticationServices.Models;

namespace ApplicationManager.Domain.AuthenticationServices.Commands
{
    public class CreateAuthenticationGrantTypeClientCredentialCommand : ICreateAuthenticationGrantTypeClientCredentialCommand
    {
        public AuthenticationGrantTypeClientCredential Execute(
            CreateAuthenticationGrantTypeClientCredentialDdto createAuthenticationGrantTypeClientCredentialDdto)
        {
            return AuthenticationGrantTypeClientCredential.Create(createAuthenticationGrantTypeClientCredentialDdto);
        }
    }
}