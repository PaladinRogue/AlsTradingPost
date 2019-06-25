using System.Net;
using Common.ApplicationServices.Exceptions;

namespace Common.Api.Exceptions
{
    public class ApplicationError
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public BusinessApplicationException Exception { get; set; }

    }
}
