using System;
using System.Threading.Tasks;

namespace PaladinRogue.Libray.Core.Application.WebRequests
{
    public interface IHttpString
    {
	    Task<string> GetAsync(Uri requestUri);

    }
}
