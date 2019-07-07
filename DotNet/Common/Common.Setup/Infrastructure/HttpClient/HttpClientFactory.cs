using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Setup.Infrastructure.Exceptions;
using Common.Setup.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Common.Setup.Infrastructure.HttpClient
{
    public class HttpClientFactory : ApplicationServices.HttpClient.IHttpClientFactory
    {
	    private readonly System.Net.Http.HttpClient _client;

		public HttpClientFactory(IOptions<ProxySettings> proxySettingsAccessor)
		{
			ProxySettings proxySettings = proxySettingsAccessor.Value;

			if (proxySettings.UseProxy)
			{
				_client = new System.Net.Http.HttpClient(new HttpClientHandler
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
				_client = new System.Net.Http.HttpClient();
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
    }
}
