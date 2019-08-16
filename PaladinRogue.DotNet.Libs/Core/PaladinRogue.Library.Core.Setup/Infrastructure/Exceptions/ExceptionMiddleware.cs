using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PaladinRogue.Library.Core.Application.Exceptions;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.Exceptions
{
    public class ExceptionMiddleware
    {
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
                        "The response has already started, the exception middleware will not be executed.");
                    throw;
                }

                context.Response.Clear();

                _logger.LogInformation(ex, "Re-written app exception");
                context.Response.StatusCode = (int)ApplicationExceptionStatusCodeMap.FromApplicationExceptionType(ex.Type);
            }
            catch (PreConditionFailedException ex)
            {
                context.Response.Clear();

                _logger.LogInformation(ex, "Re-written pre condition failed exception");
                context.Response.StatusCode = (int)ApplicationExceptionStatusCodeMap.FromApplicationExceptionType(ExceptionType.Concurrency);
            }
            catch (BadRequestException ex)
            {
                context.Response.Clear();

                _logger.LogInformation(ex, "Re-written bad request exception");
                context.Response.StatusCode = (int)ApplicationExceptionStatusCodeMap.FromApplicationExceptionType(ExceptionType.BadRequest);
            }
            catch (ServiceUnavailableExcpetion ex)
            {
                context.Response.Clear();

                _logger.LogInformation(ex, "Re-written service unavailable exception");
                context.Response.StatusCode = (int)ApplicationExceptionStatusCodeMap.FromApplicationExceptionType(ExceptionType.ServiceUnavailable);
            }
            //at this point we want to catch all uncaught exceptions and return a generic response.
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning(
                        "The response has already started, the exception middleware will not be executed.");
                    return;
                }

                _logger.LogCritical(ex, "Unhandled API exception");
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
        }
    }
}