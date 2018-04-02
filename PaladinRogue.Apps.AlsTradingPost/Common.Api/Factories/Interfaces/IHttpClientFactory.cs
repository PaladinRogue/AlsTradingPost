using System;
using System.Threading.Tasks;

namespace Common.Api.Factories.Interfaces
{
    public interface IHttpClientFactory
    {
	    Task<string> GetStringAsync(Uri requestUri);
	}
}
