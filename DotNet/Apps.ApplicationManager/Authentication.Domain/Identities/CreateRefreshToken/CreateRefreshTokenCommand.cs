using System.Threading.Tasks;
using Authentication.Domain.AuthenticationServices;

namespace Authentication.Domain.Identities.CreateRefreshToken
{
    public class CreateRefreshTokenCommand : ICreateRefreshTokenCommand
    {
        public Task<RefreshTokenIdentityDdto> ExecuteAsync(Identity identity,
            AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken)
        {
            identity.CreateRefreshToken(authenticationGrantTypeRefreshToken, out string token);

            return Task.FromResult(new RefreshTokenIdentityDdto
            {
                Token = token
            });
        }
    }
}