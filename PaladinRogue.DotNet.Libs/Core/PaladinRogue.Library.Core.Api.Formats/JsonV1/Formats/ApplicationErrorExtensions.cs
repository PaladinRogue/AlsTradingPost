using PaladinRogue.Library.Core.Api.Exceptions;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1.Formats
{
    public static class ApplicationErrorExtensions
    {
        public static Error FormatError(this ApplicationError applicationError)
        {
            return new Error
            {
                Status = (int) applicationError.HttpStatusCode,
                Code = applicationError.Exception.Code,
                Title = applicationError.Exception.Message
            };
        }
    }
}