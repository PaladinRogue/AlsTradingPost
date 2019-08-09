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

namespace Authentication.Setup.Infrastructure.Authentication.Google
{
    public class GoogleAuthenticationValidator : IGoogleAuthenticationValidator
    {
        private readonly IHttpJson _httpJson;

        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public GoogleAuthenticationValidator(IHttpJson httpJson)
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
            AuthenticationGrantTypeGoogle authenticationGrantTypeGoogle,
            ValidateClientCredentialAdto validateClientCredentialAdto)
        {
            try
            {
                GoogleAccessTokenResponse appAccessTokenResponse = await _httpJson.PostAsync<GoogleAccessTokenResponse>(
                    new Uri(authenticationGrantTypeGoogle.GrantAccessTokenUrl.Format(
                        DictionaryBuilder<string, object>.Create()
                            .Add("clientId", authenticationGrantTypeGoogle.ClientId)
                            .Add("clientSecret", authenticationGrantTypeGoogle.ClientSecret)
                            .Add("redirectUri", validateClientCredentialAdto.RedirectUri)
                            .Add("code", validateClientCredentialAdto.Token)
                            .Build()
                    )), _jsonSerializerSettings
                );

                ValidateAccessTokenResponse validateAccessTokenResponse = await _httpJson.GetAsync<ValidateAccessTokenResponse>(new Uri(
                    authenticationGrantTypeGoogle.ValidateAccessTokenUrl.Format(
                        DictionaryBuilder<string, object>.Create()
                            .Add("accessToken", appAccessTokenResponse.AccessToken)
                            .Build()
                    )), _jsonSerializerSettings);

                return ClientCredentialAuthenticationResult.Succeed(validateAccessTokenResponse.Id);
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