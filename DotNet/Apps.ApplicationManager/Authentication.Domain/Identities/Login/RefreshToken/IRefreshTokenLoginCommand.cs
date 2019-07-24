using System.Threading.Tasks;

namespace Authentication.Domain.Identities.Login.RefreshToken
{
    public interface IRefreshTokenLoginCommand
    {
        Task<Identity> ExecuteAsync(RefreshTokenLoginCommandDdto refreshTokenLoginCommandDdto);
    }
}