using ApplicationManager.Domain.AuthenticationServices;

namespace ApplicationManager.Domain.Identities.CreateRefreshToken
{
    public class CreateRefreshTokenCommand : ICreateRefreshTokenCommand
    {
        public RefreshTokenIdentityDdto Execute(Identity identity, AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken)
        {
            identity.CreateRefreshToken(authenticationGrantTypeRefreshToken, out string token);

            return new RefreshTokenIdentityDdto
            {
                Token = token
            };
        }
    }

    public class RefreshTokenIdentityDdto
    {
        public string Token { get; set; }
    }
}