using System;
using System.Threading.Tasks;
using Authentication.Application.Authentication.ClientCredential;
using Authentication.Domain.AuthenticationServices;
using Common.Application.WebRequests;
using Common.Resources.Builders.Dictionaries;
using Common.Resources.Extensions;
using Common.Setup.Infrastructure.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Authentication.Setup.Infrastructure.Authentication.Facebook
{
    public class FacebookAuthenticationValidator : IFacebookAuthenticationValidator
    {
        private readonly IHttpJson _httpJson;

        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public FacebookAuthenticationValidator(IHttpJson httpJson)
        {
            _httpJson = httpJson;
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            };
        }

        public async Task<IClientCredentialAuthenticationResult> Validate(
            AuthenticationGrantTypeFacebook authenticationGrantTypeFacebook,
            ValidateClientCredentialAdto validateClientCredentialAdto)
        {
            try
            {
                FacebookAccessTokenResponse appAccessTokenResponse = await _httpJson.GetAsync<FacebookAccessTokenResponse>(
                    new Uri(authenticationGrantTypeFacebook.GrantAccessTokenUrl.Format(
                        DictionaryBuilder<string, object>.Create()
                            .Add("clientId", authenticationGrantTypeFacebook.ClientId)
                            .Add("clientSecret", authenticationGrantTypeFacebook.ClientSecret)
                            .Add("redirectUri", validateClientCredentialAdto.RedirectUri)
                            .Add("code", validateClientCredentialAdto.Token)
                            .Build()
                    )), _jsonSerializerSettings
                );

                ValidateAccessTokenResponse validateAccessTokenResponse = await _httpJson.GetAsync<ValidateAccessTokenResponse>(new Uri(
                    authenticationGrantTypeFacebook.ValidateAccessTokenUrl.Format(
                        DictionaryBuilder<string, object>.Create()
                            .Add("inputToken", appAccessTokenResponse.AccessToken)
                            .Add("accessToken", authenticationGrantTypeFacebook.AppAccessToken)
                            .Build()
                    )), _jsonSerializerSettings);

                return validateAccessTokenResponse.Data.IsValid ? ClientCredentialAuthenticationResult.Succeed(validateAccessTokenResponse.Data.UserId) : ClientCredentialAuthenticationResult.Fail;
            }
            catch (BadRequestException)
            {
                return ClientCredentialAuthenticationResult.Fail;
            }
            catch (ServiceUnavailableExcpetion)
            {
                return ClientCredentialAuthenticationResult.Fail;
            }
        }
    }
}