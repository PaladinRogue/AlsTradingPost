using System;
using Common.Application.Exceptions;
using Common.Setup.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Api.Exceptions
{
    public class BusinessApplicationExceptionFilter : IExceptionFilter
    {
        private readonly IApplicationErrorFormatter<IFormattedError> _applicationErrorFormatter;

        public BusinessApplicationExceptionFilter(IApplicationErrorFormatter<IFormattedError> applicationErrorFormatter)
        {
            _applicationErrorFormatter = applicationErrorFormatter;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BusinessApplicationException businessApplicationException)
            {
                ApplicationError applicationError = new ApplicationError
                {
                    Exception = businessApplicationException,
                    HttpStatusCode = ApplicationExceptionStatusCodeMap.FromApplicationExceptionType(businessApplicationException.Type)
                };
                context.ExceptionHandled = true;

                switch (businessApplicationException.Type)
                {
                    case ExceptionType.BadRequest:
                        context.Result = new BadRequestObjectResult(_applicationErrorFormatter.Format(applicationError));
                        break;
                    case ExceptionType.Unknown:
                    case ExceptionType.Concurrency:
                    case ExceptionType.Unauthorized:
                    case ExceptionType.Conflict:
                    case ExceptionType.NotFound:
                        context.Result = new StatusCodeResult((int)applicationError.HttpStatusCode);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
