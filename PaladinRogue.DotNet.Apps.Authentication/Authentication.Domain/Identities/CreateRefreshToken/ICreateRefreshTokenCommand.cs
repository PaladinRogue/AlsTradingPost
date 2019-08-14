using System.Threading.Tasks;
using PaladinRogue.Authentication.Domain.AuthenticationServices;

namespace PaladinRogue.Authentication.Domain.Identities.CreateRefreshToken
{
    public interface ICreateRefreshTokenCommand
    {
        Task<RefreshTokenIdentityDdto> ExecuteAsync(Identity identity,
            AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken);
    }
}