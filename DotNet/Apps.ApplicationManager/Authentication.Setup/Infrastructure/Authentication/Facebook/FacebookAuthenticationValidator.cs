using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PaladinRogue.Authentication.Application.Authentication.ClientCredential;
using PaladinRogue.Authentication.Domain.AuthenticationServices;
using PaladinRogue.Libray.Core.Application.WebRequests;
using PaladinRogue.Libray.Core.Common.Builders.Dictionaries;
using PaladinRogue.Libray.Core.Common.Extensions;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Exceptions;

namespace PaladinRogue.Authentication.Setup.Infrastructure.Authentication.Facebook
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