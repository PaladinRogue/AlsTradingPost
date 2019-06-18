using System;
using System.Linq;
using Common.Api.Builders.Resource;
using Common.Api.Concurrency.Interfaces;
using Common.ApplicationServices.Concurrency;
using Common.Domain.Concurrency.Interfaces;
using Common.Setup.Infrastructure.Constants;
using Common.Setup.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Common.Api.Concurrency
{
    public class ConcurrencyActionFilter : IActionFilter
    {
        private const HttpVerb OnActionExecutingVerbs = HttpVerb.Put | HttpVerb.Post | HttpVerb.Delete;

        private const HttpVerb OnActionExecutedVerbs = HttpVerb.Get | HttpVerb.Put | HttpVerb.Post;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (OnActionExecutingVerbs.HasFlag(HttpVerbMapper.GetVerb(context.HttpContext.Request.Method)))
            {
                IVersioned<IConcurrencyVersion> resource = context.ActionArguments.Values.OfType<IVersioned<IConcurrencyVersion>>().SingleOrDefault();

                string concurrencyValue = context.HttpContext.Request.Headers[ConcurrencyHeaders.IfMatch];

                if (concurrencyValue == null && resource != null) throw new PreConditionFailedException();

                if (resource != null)
                {
                    resource.Version = ConcurrencyVersionFactory.CreateFromBase64String(concurrencyValue);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (OnActionExecutedVerbs.HasFlag(HttpVerbMapper.GetVerb(context.HttpContext.Request.Method)))
            {
                if (context.Result is ObjectResult result)
                {
                    if (result.Value is BuiltResource builtResource)
                    {
                        if (builtResource.Version != null)
                        {
                            string jsonVersion = JsonConvert.SerializeObject(builtResource.Version);
                            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(jsonVersion);
                            context.HttpContext.Response.Headers[ConcurrencyHeaders.ETag] = Convert.ToBase64String(plainTextBytes);
                        }
                    }
                }
            }
        }
    }
}
