using Common.Api.Settings;
using Microsoft.Extensions.Options;

namespace Common.Api.Links
{
    public class AuthenticationAbsoluteLinkProvider : IAbsoluteLinkProvider
    {
        private readonly AppSettings _appSettings;

        public AuthenticationAbsoluteLinkProvider(IOptions<AppSettings> appSettingsAccessor)
        {
            _appSettings = appSettingsAccessor.Value;
        }

        public string GetAbsoluteUrl()
        {
            return _appSettings.AuthenticationUrl;
        }
    }
}
