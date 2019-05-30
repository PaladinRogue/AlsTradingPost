using System.Collections.Generic;
using System.Net;
using Common.Application.Exceptions;

namespace Common.Setup.Infrastructure.Exceptions
{
    public static class ApplicationExceptionStatusCodeMap
    {
        private static readonly Dictionary<ExceptionType, HttpStatusCode> ExceptionTypeDictionary = new Dictionary<ExceptionType, HttpStatusCode>
        {
            { ExceptionType.Unknown, HttpStatusCode.OK },
            { ExceptionType.Concurrency, HttpStatusCode.PreconditionFailed },
            { ExceptionType.BadRequest, HttpStatusCode.BadRequest },
            { ExceptionType.Unauthorized, HttpStatusCode.Unauthorized }
        };

        public static HttpStatusCode FromApplicationExceptionType(ExceptionType exceptionType)
        {
            return ExceptionTypeDictionary[exceptionType];
        }
    }
}
