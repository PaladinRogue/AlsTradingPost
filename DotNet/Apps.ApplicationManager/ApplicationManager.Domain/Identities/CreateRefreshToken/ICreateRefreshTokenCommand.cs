using System.Threading.Tasks;
using ApplicationManager.Domain.AuthenticationServices;

namespace ApplicationManager.Domain.Identities.CreateRefreshToken
{
    public interface ICreateRefreshTokenCommand
    {
        Task<RefreshTokenIdentityDdto> ExecuteAsync(Identity identity,
            AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken);
    }
}