using System.Threading.Tasks;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models.Facebook;

namespace PaladinRogue.Authentication.Application.AuthenticationServices
{
    public interface IFacebookAuthenticationServiceApplicationService
    {
        Task<FacebookAdto> CreateAsync(CreateFacebookAdto createFacebookAdto);

        Task<FacebookAdto> GetAsync(GetFacebookAdto getFacebookAdto);

        Task<FacebookAdto> ChangeAsync(ChangeFacebookAdto changeFacebookAdto);
    }
}