using System;
using System.Threading.Tasks;

namespace PaladinRogue.Library.Core.Application.WebRequests
{
    public interface IHttpString
    {
	    Task<string> GetAsync(Uri requestUri);

    }
}
