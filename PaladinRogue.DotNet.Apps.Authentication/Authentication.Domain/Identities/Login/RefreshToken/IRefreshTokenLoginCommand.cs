using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.Login.RefreshToken
{
    public interface IRefreshTokenLoginCommand
    {
        Task<Identity> ExecuteAsync(RefreshTokenLoginCommandDdto refreshTokenLoginCommandDdto);
    }
}