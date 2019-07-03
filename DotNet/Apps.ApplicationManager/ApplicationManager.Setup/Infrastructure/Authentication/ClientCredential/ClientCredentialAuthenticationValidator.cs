using System;
using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.Authentication.ClientCredential;
using ApplicationManager.Domain.AuthenticationServices;
using Common.Api.HttpClient.Interfaces;
using Common.Resources.Builders.Dictionaries;
using Common.Resources.Extensions;
using Newtonsoft.Json;

namespace ApplicationManager.Setup.Infrastructure.Authentication.ClientCredential
{
    public class ClientCredentialAuthenticationValidator : IClientCredentialAuthenticationValidator
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ClientCredentialAuthenticationValidator(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IClientCredentialAuthenticationResult> Validate(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            ValidateClientCredentialAdto validateClientCredentialAdto)
        {

            string appAccessTokenResponse = await _httpClientFactory.GetStringAsync(new Uri(authenticationGrantTypeClientCredential.GrantAccessTokenUrl.Format(
                DictionaryBuilder<string, object>.Create()
                    .Add("clientId", authenticationGrantTypeClientCredential.ClientId)
                    .Add("clientSecret", authenticationGrantTypeClientCredential.ClientSecret)
                    .Add("redirectUri", validateClientCredentialAdto.RedirectUri)
                    .Add("code", validateClientCredentialAdto.Token)
                    .Build()
                ))

            );

            throw new NotImplementedException();

//            FacebookAppAccessToken appAccessToken =
//                JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
//
//            string userAccessTokenValidationResponse = await _httpClientFactory.GetStringAsync(new Uri(string.Format(
//                    _fbAuthSettings.AccessTokenValidationEndpoint,
//                    template.AccessToken,
//                    appAccessToken.AccessToken
//                ))
//            );
//
//            FacebookUserAccessTokenValidation userAccessTokenValidation =
//                JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

        }
    }
}