using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Common.ApplicationServices.WebRequests;
using Common.Setup.Infrastructure.Exceptions;
using Common.Setup.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Common.Setup.Infrastructure.WebRequests
{
    public class HttpClientFactory : IHttpClientFactory
    {
	    private readonly HttpClient _client;

		public HttpClientFactory(IOptions<ProxySettings> proxySettingsAccessor)
		{
			ProxySettings proxySettings = proxySettingsAccessor.Value;

			if (proxySettings.UseProxy)
			{
				_client = new HttpClient(new HttpClientHandler
				{
					Proxy = new WebProxy(proxySettings.ProxyServer, false)
					{
						UseDefaultCredentials = true
					},
					PreAuthenticate = true,
					UseDefaultCredentials = true
				});
			}
			else
			{
				_client = new HttpClient();
			}
		}

		public async Task<string> GetStringAsync(Uri requestUri)
		{
			try
			{
				return await _client.GetStringAsync(requestUri);
			}
			catch (HttpRequestException e)
			{
				throw new BadRequestException($"Failed to get a successful response from {requestUri}", e);
			}
		}

		public async Task<T> GetJsonAsync<T>(Uri requestUri, JsonSerializerSettings jsonSerializerSettings)
	    {
		    try
		    {
			    string data = await _client.GetStringAsync(requestUri);

			    T jsonData = JsonConvert.DeserializeObject<T>(data, jsonSerializerSettings);

			    return jsonData;
		    }
            catch (HttpRequestException e)
            {
			    throw new BadRequestException($"Failed to get a successful response from {requestUri}", e);
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
			    throw new BadRequestException($"Failed to get a successful response from {httpRequestMessage.RequestUri}", e);
		    }
	    }
    }
}
