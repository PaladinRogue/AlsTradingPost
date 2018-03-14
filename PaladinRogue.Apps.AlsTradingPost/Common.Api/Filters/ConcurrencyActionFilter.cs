using System;
using System.Linq;
using Common.Api.Constants;
using Common.Api.Exceptions;
using Common.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Api.Filters
{
    public class ConcurrencyActionFilter : IActionFilter
    {
        private readonly string[] _onActionExecutingVerbs =
        {
            HttpVerbs.Put, HttpVerbs.Delete
        };

        private readonly string[] _onActionExecutedVerbs =
        {
            HttpVerbs.Get, HttpVerbs.Put, HttpVerbs.Post
        };

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (_onActionExecutingVerbs.Contains(context.HttpContext.Request.Method))
            {
                string concurrencyValue = context.HttpContext.Request.Headers[ConcurrencyHeaders.IfMatch];

                if(concurrencyValue == null) throw new PreConditionFailedException();

                object resourceObj = context.ActionArguments.Values.OfType<IVersionedRequest>().FirstOrDefault();
                if (resourceObj == null) throw new Exception("Request object does not implement IVersionedRequest");

                var resource = (IVersionedRequest)resourceObj;
                int.TryParse(concurrencyValue, out var concurrencyResult);
                resource.Version = concurrencyResult;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_onActionExecutedVerbs.Contains(context.HttpContext.Request.Method))
            {
                if (context.Result is ObjectResult result)
                {
                    if (result.Value is IVersionedResource resource)
                    {
                        context.HttpContext.Response.Headers[ConcurrencyHeaders.ETag] = resource.Version.ToString();

                    }
                }
            }
        }
    }
}
