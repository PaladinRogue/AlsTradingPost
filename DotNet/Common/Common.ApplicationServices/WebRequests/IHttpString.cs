using System;
using System.Threading.Tasks;

namespace Common.ApplicationServices.WebRequests
{
    public interface IHttpString
    {
	    Task<string> GetAsync(Uri requestUri);

    }
}
