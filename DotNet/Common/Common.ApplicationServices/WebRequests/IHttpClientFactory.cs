using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.ApplicationServices.WebRequests
{
    public interface IHttpClientFactory
    {
	    Task<string> GetStringAsync(Uri requestUri);

	    Task<T> GetJsonAsync<T>(Uri requestUri, JsonSerializerSettings jsonSerializerSettings);

	    Task<HttpResponseMessage> SendAsync(
		    HttpRequestMessage httpRequestMessage,
		    HttpCompletionOption completionOption,
		    CancellationToken cancellationToken);
    }
}
