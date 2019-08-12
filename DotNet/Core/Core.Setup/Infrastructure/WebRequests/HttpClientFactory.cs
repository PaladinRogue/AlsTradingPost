using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PaladinRogue.Library.Core.Application.WebRequests;
using PaladinRogue.Library.Core.Setup.Infrastructure.Exceptions;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.WebRequests
{
    public class HttpClientFactory : IHttpJson, IHttpRequest, IHttpString
    {
        private readonly HttpClient _client;

        public HttpClientFactory()
        {
            _client = new HttpClient();
        }

        public async Task<string> GetAsync(Uri requestUri)
        {
            try
            {
                return await _client.GetStringAsync(requestUri);
            }
            catch (HttpRequestException e)
            {
                throw new ServiceUnavailableExcpetion($"Failed to get a successful response from {requestUri}", e);
            }
        }

        public async Task<T> GetAsync<T>(
            Uri requestUri,
            JsonSerializerSettings jsonSerializerSettings)
        {
            try
            {
                string data = await _client.GetStringAsync(requestUri);

                T jsonData = JsonConvert.DeserializeObject<T>(data, jsonSerializerSettings);

                return jsonData;
            }
            catch (HttpRequestException e)
            {
                throw new ServiceUnavailableExcpetion($"Failed to get a successful response from {requestUri}", e);
            }
        }

        public Task<T> PostAsync<T>(Uri requestUri, JsonSerializerSettings jsonSerializerSettings)
        {
            return PostAsync<object, T>(requestUri, null, jsonSerializerSettings);
        }

        public async Task<TOut> PostAsync<TIn, TOut>(
            Uri requestUri,
            TIn request,
            JsonSerializerSettings jsonSerializerSettings)
        {
            try
            {
                StringContent stringContent = null;
                if (request != null)
                {
                    string requestString = JsonConvert.SerializeObject(request);
                    stringContent = new StringContent(requestString);
                }

                HttpResponseMessage response = await _client.PostAsync(requestUri, stringContent);

                string readAsStringAsync = await response.Content.ReadAsStringAsync();
                TOut jsonData = JsonConvert.DeserializeObject<TOut>(readAsStringAsync, jsonSerializerSettings);

                return jsonData;
            }
            catch (HttpRequestException e)
            {
                throw new ServiceUnavailableExcpetion($"Failed to get a successful response from {requestUri}", e);
            }
        }

        public async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage httpRequestMessage,
            HttpCompletionOption completionOption,
            CancellationToken cancellationToken)
        {
            try
            {
                return await _client.SendAsync(httpRequestMessage, completionOption, cancellationToken);
            }
            catch (HttpRequestException e)
            {
                throw new ServiceUnavailableExcpetion($"Failed to get a successful response from {httpRequestMessage.RequestUri}", e);
            }
        }
    }
}