using System.Threading.Tasks;
using Authentication.Domain.AuthenticationServices;

namespace Authentication.Domain.Identities.CreateRefreshToken
{
    public interface ICreateRefreshTokenCommand
    {
        Task<RefreshTokenIdentityDdto> ExecuteAsync(Identity identity,
            AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken);
    }
}