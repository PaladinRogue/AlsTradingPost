using System.Threading.Tasks;
using Authentication.Application.AuthenticationServices.Models.Facebook;

namespace Authentication.Application.AuthenticationServices
{
    public interface IFacebookAuthenticationServiceApplicationService
    {
        Task<FacebookAdto> CreateAsync(CreateFacebookAdto createFacebookAdto);

        Task<FacebookAdto> GetAsync(GetFacebookAdto getFacebookAdto);

        Task<FacebookAdto> ChangeAsync(ChangeFacebookAdto changeFacebookAdto);
    }
}