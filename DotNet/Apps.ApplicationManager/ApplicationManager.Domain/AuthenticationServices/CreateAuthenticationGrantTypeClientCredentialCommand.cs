namespace ApplicationManager.Domain.AuthenticationServices
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