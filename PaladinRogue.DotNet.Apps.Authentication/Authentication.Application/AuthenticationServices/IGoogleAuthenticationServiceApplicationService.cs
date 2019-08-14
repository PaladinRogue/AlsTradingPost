using System.Threading.Tasks;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models.Google;

namespace PaladinRogue.Authentication.Application.AuthenticationServices
{
    public interface IGoogleAuthenticationServiceApplicationService
    {
        Task<GoogleAdto> CreateAsync(CreateGoogleAdto createGoogleAdto);

        Task<GoogleAdto> GetAsync(GetGoogleAdto getGoogleAdto);

        Task<GoogleAdto> ChangeAsync(ChangeGoogleAdto changeGoogleAdto);
    }
}