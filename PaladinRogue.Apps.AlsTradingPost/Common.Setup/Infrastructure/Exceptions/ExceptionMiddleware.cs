using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Common.Setup.Infrastructure.Exceptions
{
    public class ExceptionMiddleware
    {
        private static readonly Dictionary<ExceptionType, HttpStatusCode> ExceptionTypeDictionary = new Dictionary<ExceptionType, HttpStatusCode>
        {
            { ExceptionType.None, HttpStatusCode.OK },
            { ExceptionType.Concurrency, HttpStatusCode.PreconditionFailed },
            { ExceptionType.BadRequest, HttpStatusCode.BadRequest },
            { ExceptionType.Unauthorized, HttpStatusCode.Unauthorized }
        };

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessApplicationException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning(
                        "The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                context.Response.Clear();

                _logger.LogInformation(ex, "Re-written app exception");
                context.Response.StatusCode = (int) ExceptionTypeDictionary[ex.Type];

                await context.Response.WriteAsync(ex.Message);
            }
            catch (PreConditionFailedException ex)
            {
                context.Response.Clear();

                _logger.LogInformation(ex, "Re-written pre condition failed exception");
                context.Response.StatusCode = (int) ExceptionTypeDictionary[ExceptionType.Concurrency];
            }
            catch (BadRequestException ex)
            {
                context.Response.Clear();

                _logger.LogInformation(ex, "Re-written bad request exception");
                context.Response.StatusCode = (int) ExceptionTypeDictionary[ExceptionType.BadRequest];
            }
            //at this point we want to catch all uncaught exceptions and return a generic response.
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Unhandled API exception");
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
        }
    }
}