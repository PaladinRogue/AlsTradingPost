using System.Net;
using PaladinRogue.Library.Core.Application.Exceptions;

namespace PaladinRogue.Library.Core.Api.Exceptions
{
    public class ApplicationError
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public BusinessApplicationException Exception { get; set; }
    }
}
