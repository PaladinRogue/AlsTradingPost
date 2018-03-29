using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Api.Factories.Interfaces;
using Common.Api.Settings;
using Microsoft.Extensions.Options;

namespace Common.Api.Factories
{
    public class HttpClientFactory : IHttpClientFactory
    {
	    private readonly HttpClient _client;

		public HttpClientFactory(IOptions<ProxySettings> proxySettingsAccessor)
		{
			var proxySettings = proxySettingsAccessor.Value;

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

	    public Task<string> GetStringAsync(string requestUri)
	    {
		    return _client.GetStringAsync(requestUri);
	    }
    }
}
