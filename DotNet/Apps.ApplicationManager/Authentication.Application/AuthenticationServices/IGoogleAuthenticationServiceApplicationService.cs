using System.Threading.Tasks;
using Authentication.Application.AuthenticationServices.Models.Google;

namespace Authentication.Application.AuthenticationServices
{
    public interface IGoogleAuthenticationServiceApplicationService
    {
        Task<GoogleAdto> CreateAsync(CreateGoogleAdto createGoogleAdto);

        Task<GoogleAdto> GetAsync(GetGoogleAdto getGoogleAdto);

        Task<GoogleAdto> ChangeAsync(ChangeGoogleAdto changeGoogleAdto);
    }
}