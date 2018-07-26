using System;
using Common.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Api.Exceptions
{
    public class BusinessApplicationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BusinessApplicationException businessApplicationException)
            {
                switch (businessApplicationException.Type)
                {
                    case ExceptionType.BadRequest:
                        context.ExceptionHandled = true;
                        context.Result = new BadRequestObjectResult(businessApplicationException.Message);
                        break;
                    case ExceptionType.None:
                    case ExceptionType.Concurrency:
                    case ExceptionType.Unauthorized:
                        context.ExceptionHandled = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
