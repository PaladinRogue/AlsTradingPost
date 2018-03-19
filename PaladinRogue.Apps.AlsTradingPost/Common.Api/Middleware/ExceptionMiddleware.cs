using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Api.Exceptions;
using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Common.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private static readonly Dictionary<ExceptionType, HttpStatusCode> ExceptionTypeDictionary = new Dictionary<ExceptionType, HttpStatusCode>
        {
            { ExceptionType.None, HttpStatusCode.OK }
        };

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {
                if (context.Response.HasStarted)
                {
                    //todo logging
//                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                context.Response.Clear();
                
                //todo logging
                context.Response.StatusCode = (int) ExceptionTypeDictionary[ex.Type];

                await context.Response.WriteAsync(ex.Message);
            }
            catch (PreConditionFailedException ex)
            {
                //todo logging
                context.Response.StatusCode = (int) HttpStatusCode.PreconditionFailed;
            }
            //at this point we want to catch all uncaught exceptions and return a generic response.
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception ex)
            {
                //todo logging
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
        }
    }
}