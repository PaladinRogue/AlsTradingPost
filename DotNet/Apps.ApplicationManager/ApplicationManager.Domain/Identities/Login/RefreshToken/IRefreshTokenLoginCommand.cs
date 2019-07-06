using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.Login.RefreshToken
{
    public interface IRefreshTokenLoginCommand
    {
        Task<Identity> ExecuteAsync(RefreshTokenLoginCommandDdto refreshTokenLoginCommandDdto);
    }
}