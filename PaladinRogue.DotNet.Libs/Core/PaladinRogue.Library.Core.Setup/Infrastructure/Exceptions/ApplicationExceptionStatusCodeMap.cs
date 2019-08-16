using System.Collections.Generic;
using System.Net;
using PaladinRogue.Library.Core.Application.Exceptions;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.Exceptions
{
    public static class ApplicationExceptionStatusCodeMap
    {
        private static readonly Dictionary<ExceptionType, HttpStatusCode> ExceptionTypeDictionary = new Dictionary<ExceptionType, HttpStatusCode>
        {
            { ExceptionType.Unknown, HttpStatusCode.InternalServerError },
            { ExceptionType.ServiceUnavailable, HttpStatusCode.ServiceUnavailable },
            { ExceptionType.Concurrency, HttpStatusCode.PreconditionFailed },
            { ExceptionType.BadRequest, HttpStatusCode.BadRequest },
            { ExceptionType.Unauthorized, HttpStatusCode.Unauthorized },
            { ExceptionType.Conflict, HttpStatusCode.Conflict },
            { ExceptionType.NotFound, HttpStatusCode.NotFound }
        };

        public static HttpStatusCode FromApplicationExceptionType(ExceptionType exceptionType)
        {
            return ExceptionTypeDictionary[exceptionType];
        }
    }
}
