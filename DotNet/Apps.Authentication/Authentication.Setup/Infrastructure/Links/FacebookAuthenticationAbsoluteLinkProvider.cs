using Authentication.Setup.Settings;
using Common.Api.Links;
using Microsoft.Extensions.Options;

namespace Authentication.Setup.Infrastructure.Links
{
    public class FacebookAuthenticationAbsoluteLinkProvider : IAbsoluteLinkProvider
    {
        private readonly FacebookAuthSettings _facebookAuthSettings;

        public FacebookAuthenticationAbsoluteLinkProvider(IOptions<FacebookAuthSettings> facebookAuthSettingsAccessor)
        {
            _facebookAuthSettings = facebookAuthSettingsAccessor.Value;
        }

        public string GetAbsoluteUrl()
        {
            return string.Format(_facebookAuthSettings.AccessTokenEndpoint, _facebookAuthSettings.AppId);
        }
    }
}
