using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PaladinRogue.Library.Core.Application.WebRequests
{
    public interface IHttpRequest
    {
        Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage httpRequestMessage,
            HttpCompletionOption completionOption,
            CancellationToken cancellationToken);
    }
}