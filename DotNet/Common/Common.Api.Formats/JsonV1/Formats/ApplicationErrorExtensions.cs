using Common.Api.Exceptions;

namespace Common.Api.Formats.JsonV1.Formats
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