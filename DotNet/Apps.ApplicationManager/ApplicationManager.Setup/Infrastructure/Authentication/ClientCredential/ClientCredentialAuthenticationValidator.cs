using System;
using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.Authentication.ClientCredential;
using ApplicationManager.Domain.AuthenticationServices;
using Common.ApplicationServices.WebRequests;
using Common.Resources.Builders.Dictionaries;
using Common.Resources.Extensions;
using Common.Setup.Infrastructure.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ApplicationManager.Setup.Infrastructure.Authentication.ClientCredential
{
    public class ClientCredentialAuthenticationValidator : IClientCredentialAuthenticationValidator
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public ClientCredentialAuthenticationValidator(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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
            AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            ValidateClientCredentialAdto validateClientCredentialAdto)
        {
            try
            {
                ClientCredentialAccessTokenResponse appAccessTokenResponse = await _httpClientFactory.GetJsonAsync<ClientCredentialAccessTokenResponse>(
                    new Uri(authenticationGrantTypeClientCredential.GrantAccessTokenUrl.Format(
                        DictionaryBuilder<string, object>.Create()
                            .Add("clientId", authenticationGrantTypeClientCredential.ClientId)
                            .Add("clientSecret", authenticationGrantTypeClientCredential.ClientSecret)
                            .Add("redirectUri", validateClientCredentialAdto.RedirectUri)
                            .Add("code", validateClientCredentialAdto.Token)
                            .Build()
                    )), _jsonSerializerSettings
                );

                ValidateAccessTokenResponse validateAccessTokenResponse = await _httpClientFactory.GetJsonAsync<ValidateAccessTokenResponse>(new Uri(authenticationGrantTypeClientCredential.ValidateAccessTokenUrl.Format(
                    DictionaryBuilder<string, object>.Create()
                        .Add("inputToken", appAccessTokenResponse.AccessToken)
                        .Add("accessToken", authenticationGrantTypeClientCredential.AppAccessToken)
                        .Build()
                )), _jsonSerializerSettings);

                return validateAccessTokenResponse.Data.IsValid ?
                    ClientCredentialAuthenticationResult.Succeed(validateAccessTokenResponse.Data.UserId) : ClientCredentialAuthenticationResult.Fail;
            }
            catch (BadRequestException)
            {
                return ClientCredentialAuthenticationResult.Fail;
            }
        }
    }
}