namespace ApplicationManager.Domain.AuthenticationServices.CreateClientCredential
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