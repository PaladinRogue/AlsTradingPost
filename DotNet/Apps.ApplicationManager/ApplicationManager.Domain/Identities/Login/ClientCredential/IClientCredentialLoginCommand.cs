using ApplicationManager.Domain.AuthenticationServices;

namespace ApplicationManager.Domain.Identities.Login.ClientCredential
{
    public interface IClientCredentialLoginCommand
    {
        Identity Execute(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            ClientCredentialLoginCommandDdto clientCredentialLoginCommandDdto);
    }
}