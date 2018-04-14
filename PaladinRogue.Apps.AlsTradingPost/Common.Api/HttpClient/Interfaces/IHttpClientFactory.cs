using System;
using System.Threading.Tasks;

namespace Common.Api.HttpClient.Interfaces
{
    public interface IHttpClientFactory
    {
	    Task<string> GetStringAsync(Uri requestUri);
	}
}
