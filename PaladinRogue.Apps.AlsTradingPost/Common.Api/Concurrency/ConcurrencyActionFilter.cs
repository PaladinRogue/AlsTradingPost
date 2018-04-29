using System;
using System.Linq;
using Common.Api.Concurrency.Interfaces;
using Common.Resources.Concurrency;
using Common.Setup.Infrastructure.Constants;
using Common.Setup.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Common.Api.Concurrency
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

                object resourceObj = context.ActionArguments.Values.OfType<IVersionedTemplate>().SingleOrDefault();
                if (resourceObj == null) throw new Exception("Request object does not implement IVersionedTemplate");

                IVersionedTemplate resource = (IVersionedTemplate)resourceObj;

                resource.Version = ConcurrencyVersionFactory.CreateFromBase64String(concurrencyValue);
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
                        string jsonVersion = JsonConvert.SerializeObject(resource.Version);
                        byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(jsonVersion);
                        context.HttpContext.Response.Headers[ConcurrencyHeaders.ETag] = Convert.ToBase64String(plainTextBytes);
                    }
                }
            }
        }
    }
}
