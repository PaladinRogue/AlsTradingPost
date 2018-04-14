using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Api.HttpClient.Interfaces;
using Common.Api.Settings;
using Microsoft.Extensions.Options;

namespace Common.Api.HttpClient
{
    public class HttpClientFactory : IHttpClientFactory
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

	    public Task<string> GetStringAsync(Uri requestUri)
	    {
		    return _client.GetStringAsync(requestUri);
	    }
    }
}
