using System.Net;
using PaladinRogue.Libray.Core.Application.Exceptions;

namespace PaladinRogue.Libray.Core.Api.Exceptions
{
    public class ApplicationError
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public BusinessApplicationException Exception { get; set; }
    }
}
