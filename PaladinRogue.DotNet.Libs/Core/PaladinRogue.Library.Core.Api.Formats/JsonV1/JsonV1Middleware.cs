using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1
{
    public class JsonV1Middleware
    {
        private readonly RequestDelegate _next;

        public JsonV1Middleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.ContentType == MediaTypes.Json)
            {
                context.Request.ContentType = MediaTypes.JsonV1;
            }

            return _next(context);
        }
    }
}