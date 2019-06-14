namespace ApplicationManager.Domain.AuthenticationServices.CreateClientCredential
{
    public interface ICreateAuthenticationGrantTypeClientCredentialCommand
    {
        AuthenticationGrantTypeClientCredential Execute(
            CreateAuthenticationGrantTypeClientCredentialDdto createAuthenticationGrantTypeClientCredentialDdto);
    }
}