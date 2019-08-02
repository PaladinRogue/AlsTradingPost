using System.Threading.Tasks;
using Authentication.ApplicationServices.AuthenticationServices.Models.Facebook;

namespace Authentication.ApplicationServices.AuthenticationServices
{
    public interface IFacebookAuthenticationServiceApplicationService
    {
        Task<FacebookAdto> CreateAsync(CreateFacebookAdto createFacebookAdto);

        Task<FacebookAdto> GetAsync(GetFacebookAdto getFacebookAdto);

        Task<FacebookAdto> ChangeAsync(ChangeFacebookAdto changeFacebookAdto);
    }
}