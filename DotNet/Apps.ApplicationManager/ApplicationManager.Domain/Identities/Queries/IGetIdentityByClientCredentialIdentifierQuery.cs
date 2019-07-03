using ApplicationManager.Domain.AuthenticationServices;

namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IGetIdentityByClientCredentialIdentifierQuery
    {
        Identity Run(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential, string identifier);
    }
}