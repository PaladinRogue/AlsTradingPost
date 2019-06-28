using ApplicationManager.Domain.AuthenticationServices;

namespace ApplicationManager.Domain.Identities.CreateRefreshToken
{
    public interface ICreateRefreshTokenCommand
    {
        RefreshTokenIdentityDdto Execute(Identity identity, AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken);
    }
}