namespace ApplicationManager.Domain.AuthenticationServices
{
    public interface ICreateAuthenticationGrantTypeClientCredentialCommand
    {
        AuthenticationGrantTypeClientCredential Execute(
            CreateAuthenticationGrantTypeClientCredentialDdto createAuthenticationGrantTypeClientCredentialDdto);
    }
}