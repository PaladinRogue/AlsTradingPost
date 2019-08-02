using System.Threading.Tasks;
using Authentication.ApplicationServices.AuthenticationServices.Models.Google;

namespace Authentication.ApplicationServices.AuthenticationServices
{
    public interface IGoogleAuthenticationServiceApplicationService
    {
        Task<GoogleAdto> CreateAsync(CreateGoogleAdto createGoogleAdto);

        Task<GoogleAdto> GetAsync(GetGoogleAdto getGoogleAdto);

        Task<GoogleAdto> ChangeAsync(ChangeGoogleAdto changeGoogleAdto);
    }
}