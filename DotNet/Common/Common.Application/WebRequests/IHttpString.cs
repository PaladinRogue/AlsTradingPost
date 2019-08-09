using System;
using System.Threading.Tasks;

namespace Common.Application.WebRequests
{
    public interface IHttpString
    {
	    Task<string> GetAsync(Uri requestUri);

    }
}
