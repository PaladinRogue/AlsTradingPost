using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Messaging.Infrastructure.Dispatchers;
using Microsoft.AspNetCore.Http;

namespace Common.Setup.Infrastructure.Messaging
{
    public class DispatchMessagesMiddleware
    {
        private static readonly IList<HttpStatusCode> SuccesStatusCodes = new List<HttpStatusCode>
        {
            HttpStatusCode.OK,
            HttpStatusCode.Created,
            HttpStatusCode.Accepted,
            HttpStatusCode.NoContent
        };

        private readonly RequestDelegate _next;

        public DispatchMessagesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IMessageDispatcher messageDispatcher)
        {
            await _next.Invoke(context);

            if (SuccesStatusCodes.Contains((HttpStatusCode)context.Response.StatusCode))
            {
                await messageDispatcher.DispatchMessagesAsync();
            }
        }
    }
}
