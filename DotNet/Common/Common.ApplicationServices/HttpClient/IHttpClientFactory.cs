using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.ApplicationServices.HttpClient
{
    public interface IHttpClientFactory
    {
	    Task<string> GetStringAsync(Uri requestUri);

	    Task<T> GetJsonAsync<T>(Uri requestUri, JsonSerializerSettings jsonSerializerSettings);
	}
}
