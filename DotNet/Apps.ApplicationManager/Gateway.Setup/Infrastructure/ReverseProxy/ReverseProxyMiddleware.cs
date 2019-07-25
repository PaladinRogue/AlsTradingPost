using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Common.ApplicationServices.WebRequests;
using Gateway.ApplicationServices.Applications;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Gateway.Setup.Infrastructure.ReverseProxy
{
    public class ReverseProxyMiddleware
    {
        private readonly RequestDelegate _nextMiddleware;

        private IApplicationKernalService _applicationKernalService;

        public ReverseProxyMiddleware(RequestDelegate nextMiddleware)
        {
            _nextMiddleware = nextMiddleware;
        }

        public async Task Invoke(
            HttpContext context,
            IHttpClientFactory httpClientFactory,
            IApplicationKernalService applicationKernalService)
        {
            _applicationKernalService = applicationKernalService;

            Uri targetUri = await BuildTargetUriAsync(context.Request);

            if (targetUri != null)
            {
                HttpRequestMessage targetRequestMessage = CreateTargetMessage(context, targetUri);

                using (HttpResponseMessage responseMessage = await httpClientFactory.SendAsync(targetRequestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted))
                {
                    context.Response.StatusCode = (int) responseMessage.StatusCode;
                    CopyFromTargetResponseHeaders(context, responseMessage);
                    await responseMessage.Content.CopyToAsync(context.Response.Body);
                }

                return;
            }

            await _nextMiddleware(context);
        }

        private static HttpRequestMessage CreateTargetMessage(
            HttpContext context,
            Uri targetUri)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            CopyFromOriginalRequestContentAndHeaders(context, requestMessage);

            requestMessage.RequestUri = targetUri;
            requestMessage.Headers.Host = targetUri.Host;
            requestMessage.Method = GetMethod(context.Request.Method);

            return requestMessage;
        }

        private static void CopyFromOriginalRequestContentAndHeaders(
            HttpContext context,
            HttpRequestMessage requestMessage)
        {
            string requestMethod = context.Request.Method;

            if (HasBody(requestMethod))
            {
                StreamContent streamContent = new StreamContent(context.Request.Body);
                requestMessage.Content = streamContent;

                foreach ((string key, StringValues value) in context.Request.Headers)
                {
                    requestMessage.Content.Headers.TryAddWithoutValidation(key, value.ToArray());
                }
            }

            foreach ((string key, StringValues value) in context.Request.Headers)
            {
                requestMessage.Headers.TryAddWithoutValidation(key, value.ToArray());
            }
        }

        private static bool HasBody(string requestMethod)
        {
            return HttpMethods.IsPut(requestMethod) || HttpMethods.IsPost(requestMethod);
        }

        private static void CopyFromTargetResponseHeaders(
            HttpContext context,
            HttpResponseMessage responseMessage)
        {
            foreach ((string key, IEnumerable<string> value) in responseMessage.Headers)
            {
                context.Response.Headers[key] = value.ToArray();
            }

            foreach ((string key, IEnumerable<string> value) in responseMessage.Content.Headers)
            {
                context.Response.Headers[key] = value.ToArray();
            }

            context.Response.Headers.Remove("transfer-encoding");
        }

        private static HttpMethod GetMethod(string method)
        {
            if (HttpMethods.IsDelete(method)) return HttpMethod.Delete;
            if (HttpMethods.IsGet(method)) return HttpMethod.Get;
            if (HttpMethods.IsOptions(method)) return HttpMethod.Options;
            if (HttpMethods.IsPost(method)) return HttpMethod.Post;
            if (HttpMethods.IsPut(method)) return HttpMethod.Put;
            return new HttpMethod(method);
        }

        private async Task<Uri> BuildTargetUriAsync(HttpRequest request)
        {
            IList<string> remainingPathParts = request.Path.Value.Split("/", StringSplitOptions.RemoveEmptyEntries);

            if (remainingPathParts.Count <= 1) return null;

            string applicationSystemName = remainingPathParts[1];

            ApplicationAdto applicationAdto = await _applicationKernalService.GetByNameAsync(applicationSystemName);

            return applicationAdto == null ? null : new Uri(applicationAdto.HostUri, request.Path);
        }
    }
}